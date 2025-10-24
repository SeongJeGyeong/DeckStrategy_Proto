using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    private DataCenter dataCenter;

    [SerializeField]
    Character CharacterModelPrefab; // LDK : Character�� ����
    [SerializeField]
    GameObject followUIPrefab;

    [SerializeField]
    Canvas characterUICanvas;

    [SerializeField]
    BattleSystem battleSystem;

    Transform slotTransform;
    public bool isPlaced = false;

    public Character character { get; private set; } // LDK : BattleSystem���� �����ؼ� ����ϰ�; public���� ������
    //GameObject model ���� GmaeObject -> Character
    ObjectFollowUI characterFollowUI;
    CharacterUI chracterBattleUI;

    public Transform AttackedPosition;

    public OwnedCharacterInfo characterInfo;

    void Awake()
    {
        // ��� ��ư�� start�� LineupSlot�� Start���� ���� ����Ǿ� ���� �������� ���� ���� �ֱ⿡ Awake���� ȣ��
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
    }

    public void SetSelectedCharacter(OwnedCharacterInfo info, bool isEnemy)
    {
        isPlaced = true;
        character.isEnemy = isEnemy;

        CharacterData data = dataCenter.characterDataTable.FindCharacterData(info.characterID);
        CharacterModelData modelData = dataCenter.characterModelDataTable.FindCharacterModel(info.characterModelID);
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
    }

    public void ActivateBattleUI()
    {
        chracterBattleUI.gameObject.SetActive(true);
        characterFollowUI.gameObject.SetActive(false);
    }
}
