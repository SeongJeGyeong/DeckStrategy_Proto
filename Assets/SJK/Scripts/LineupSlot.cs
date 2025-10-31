using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    private DataCenter dataCenter;

    [SerializeField]
    Character CharacterModelPrefab; // LDK : Character로 수정
    [SerializeField]
    GameObject followUIPrefab;

    [SerializeField]
    Canvas characterUICanvas;

    [SerializeField]
    BattleSystem battleSystem;

    Transform slotTransform;
    public bool isPlaced = false;

    public Character character { get; private set; }

    ObjectFollowUI characterFollowUI;
    CharacterUI chracterBattleUI;

    public Transform AttackedPosition;
    public OwnedCharacterInfo characterInfo;

    public event System.Action OnCPUpdated;

    void Awake()
    {
        // 토글 버튼의 start가 LineupSlot의 Start보다 먼저 실행되어 모델을 생성하지 못할 수도 있기에 Awake에서 호출
        slotTransform = this.transform;
        character = Instantiate(CharacterModelPrefab, slotTransform);
        character.gameObject.SetActive(false);

        GameObject ui = Instantiate(followUIPrefab, characterUICanvas.transform);
        characterFollowUI = ui.GetComponentInChildren<ObjectFollowUI>();
        characterFollowUI.SetTarget(character.transform);
        characterFollowUI.gameObject.SetActive(false);

        chracterBattleUI = ui.GetComponentInChildren<CharacterUI>();
        chracterBattleUI.Init(character);
        chracterBattleUI.SetTarget(character.transform);
        chracterBattleUI.gameObject.SetActive(false);

        character.HealthComp.OnDie += DeselectCharacter;
    }

    public void SetSelectedCharacter(OwnedCharacterInfo info, bool isEnemy)
    {
        isPlaced = true;
        character.isEnemy = isEnemy;

        CharacterData data = dataCenter.FindCharacterData(info.characterID);
        CharacterModelData modelData = dataCenter.FindCharacterModel(info.characterModelID);
        if (data == null || modelData == null) return;

        characterInfo = info;
        character.characterData = data;
        character.characterModelData = modelData;
        character.GetComponent<MeshRenderer>().material.color = modelData.material.color;
        character.gameObject.SetActive(true);
        if(characterFollowUI != null)
        {
            characterFollowUI.SetCharacterInfo(data.type, info.characterLevel);
            characterFollowUI.gameObject.SetActive(true);
        }
        if(chracterBattleUI != null)
        {
        }

        character.UpdateCombatPower();
        OnCPUpdated?.Invoke();
    }

    public void DeselectCharacter()
    {
        isPlaced = false;
        character.characterData = null;
        character.characterModelData = null;
        character.gameObject.SetActive(false);

        if (characterFollowUI != null)
        {
            characterFollowUI.gameObject.SetActive(false);
        }
        if (chracterBattleUI != null)
        {
            chracterBattleUI.gameObject.SetActive(false);
        }

        character.UpdateCombatPower();
        OnCPUpdated?.Invoke();
    }

    public void ActivateBattleUI()
    {
        chracterBattleUI.Init(character);
        chracterBattleUI.gameObject.SetActive(true);
        characterFollowUI.gameObject.SetActive(false);
    }
}
