using UnityEngine;
using UnityEngine.Events;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    Character CharacterModelPrefab; // LDK : Character로 수정
    // GameObject CharacterModelPrefab; 원본
    [SerializeField]
    ObjectFollowUI followUIPrefab;

    [SerializeField]
    CharacterUI characterUIPrefab;

    [SerializeField]
    Canvas characterUICanvas;

    public CharacterBase characterBase { get; private set; }

    Transform slotTransform;
    public bool isPlaced = false;

    public Character model { get; private set; } // LDK : BattleSystem에서 접근해서 사용하고싶어서 public으로 수정함
    //GameObject model 원본 GmaeObject -> Character
    ObjectFollowUI characterFollowUI;
    CharacterUI chracterBattleUI;

    void Awake()
    {
        // 토글 버튼의 start가 LineupSlot의 Start보다 먼저 실행되어 모델을 생성하지 못할 수도 있기에 Awake에서 호출
        slotTransform = this.transform;
        model = Instantiate(CharacterModelPrefab, slotTransform);
        model.gameObject.SetActive(false);

        if(characterUIPrefab != null)
        {
            chracterBattleUI = Instantiate(characterUIPrefab, characterUICanvas.transform);
            chracterBattleUI.Init(model);
            chracterBattleUI.SetTarget(model.transform);
            chracterBattleUI.gameObject.SetActive(true);
        }
        else
        {
            characterFollowUI = Instantiate(followUIPrefab, characterUICanvas.transform);
            characterFollowUI.SetTarget(model.transform);
            characterFollowUI.gameObject.SetActive(false);
        }
    }

    public void SetSelectedCharacter(CharacterBase charBase)
    {
        isPlaced = true;
        characterBase = charBase;
        model.GetComponent<MeshRenderer>().material.color = charBase.characterModelData.material.color;
        model.gameObject.SetActive(true);
        if(characterFollowUI != null)
        {
            characterFollowUI.SetCharacterInfo(characterBase.characterData.type, characterBase.Level);
            characterFollowUI.gameObject.SetActive(true);
        }
        if(chracterBattleUI != null)
        {

        }
    }

    public void DeselectCharacter()
    {
        isPlaced = false;
        characterBase = null;
        model.gameObject.SetActive(false);
        if (characterFollowUI != null)
        {
            characterFollowUI.gameObject.SetActive(false);
        }
    }
}
