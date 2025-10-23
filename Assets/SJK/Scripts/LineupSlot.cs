using UnityEngine;
using UnityEngine.Events;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    Character CharacterModelPrefab; // LDK : Character�� ����
    // GameObject CharacterModelPrefab; ����
    [SerializeField]
    GameObject followUIPrefab;

    //[SerializeField]
    //CharacterUI characterUIPrefab;

    [SerializeField]
    Canvas characterUICanvas;

    [SerializeField]
    BattleSystem battleSystem;

    //public CharacterBase characterBase { get; private set; }

    Transform slotTransform;
    public bool isPlaced = false;

    public Character model { get; private set; } // LDK : BattleSystem���� �����ؼ� ����ϰ�; public���� ������
    //GameObject model ���� GmaeObject -> Character
    ObjectFollowUI characterFollowUI;
    CharacterUI chracterBattleUI;

    public Transform AttackedPosition;

    void Awake()
    {
        // ��� ��ư�� start�� LineupSlot�� Start���� ���� ����Ǿ� ���� �������� ���� ���� �ֱ⿡ Awake���� ȣ��
        slotTransform = this.transform;
        model = Instantiate(CharacterModelPrefab, slotTransform);
        model.gameObject.SetActive(false);

        GameObject ui = Instantiate(followUIPrefab, characterUICanvas.transform);
        characterFollowUI = ui.GetComponentInChildren<ObjectFollowUI>();
        characterFollowUI.SetTarget(model.transform);
        characterFollowUI.gameObject.SetActive(false);

        chracterBattleUI = ui.GetComponentInChildren<CharacterUI>();
        chracterBattleUI.Init(model);
        chracterBattleUI.SetTarget(model.transform);
        chracterBattleUI.gameObject.SetActive(false);
    }

    public void SetSelectedCharacter(CharacterBase charBase, bool isEnemy)
    {
        isPlaced = true;
        model.isEnemy = isEnemy;
        model.characterBase = charBase;
        model.GetComponent<MeshRenderer>().material.color = charBase.characterModelData.material.color;
        model.gameObject.SetActive(true);
        if(characterFollowUI != null)
        {
            characterFollowUI.SetCharacterInfo(model.characterBase.characterData.type, model.characterBase.Level);
            characterFollowUI.gameObject.SetActive(true);
        }
        if(chracterBattleUI != null)
        {
        }
    }

    public void DeselectCharacter()
    {
        isPlaced = false;
        model.characterBase = null;
        model.gameObject.SetActive(false);
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
