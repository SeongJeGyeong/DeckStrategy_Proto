using UnityEngine;
using UnityEngine.Events;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    GameObject CharacterModelPrefab;
    [SerializeField]
    ObjectFollowUI followUIPrefab;
    [SerializeField]
    Canvas characterUICanvas;

    public CharacterBase characterBase { get; private set; }

    Transform slotTransform;
    public bool isPlaced = false;

    GameObject model;
    ObjectFollowUI characterFollowUI;


    void Awake()
    {
        // ��� ��ư�� start�� LineupSlot�� Start���� ���� ����Ǿ� ���� �������� ���� ���� �ֱ⿡ Awake���� ȣ��
        slotTransform = this.transform;
        model = Instantiate(CharacterModelPrefab, slotTransform);
        model.SetActive(false);

        characterFollowUI = Instantiate(followUIPrefab, characterUICanvas.transform);
        characterFollowUI.SetTarget(model.transform);
        characterFollowUI.gameObject.SetActive(false);
    }

    public void SetSelectedCharacter(CharacterBase charBase)
    {
        isPlaced = true;
        characterBase = charBase;
        model.GetComponent<MeshRenderer>().material.color = charBase.characterModelData.material.color;
        model.SetActive(true);
        if(characterFollowUI != null)
        {
            characterFollowUI.SetCharacterInfo(characterBase.characterData.type, characterBase.Level);
            characterFollowUI.gameObject.SetActive(true);
        }
    }

    public void DeselectCharacter()
    {
        isPlaced = false;
        characterBase = null;
        model.SetActive(false);
        if (characterFollowUI != null)
        {
            characterFollowUI.gameObject.SetActive(false);
        }
    }
}
