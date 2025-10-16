using System.Drawing;
using UnityEngine;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    GameObject CharacterModelPrefab;

    public CharacterBase characterBase { get; private set; }

    Transform slotTransform;
    public bool isPlaced = false;

    GameObject model;

    void Awake()
    {
        // ��� ��ư�� start�� LineupSlot�� Start���� ���� ����Ǿ� ���� �������� ���� ���� �ֱ⿡ Awake���� ȣ��
        slotTransform = this.transform;
        model = Instantiate(CharacterModelPrefab, slotTransform);
        model.SetActive(false);
    }

    public void SetSelectedCharacter(CharacterBase charBase)
    {
        isPlaced = true;
        characterBase = charBase;
        model.GetComponent<MeshRenderer>().material.color = charBase.characterModelData.material.color;
        model.SetActive(true);
    }

    public void DeselectCharacter()
    {
        isPlaced = false;
        characterBase = null;
        model.SetActive(false);
    }
}
