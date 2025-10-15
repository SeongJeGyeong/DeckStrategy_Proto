using System.Drawing;
using UnityEngine;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    GameObject CharacterModelPrefab;
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

    public void SetSelectedCharacter(UnityEngine.Color color, bool selected)
    {
        isPlaced = selected;
        model.GetComponent<MeshRenderer>().material.color = selected ? color : UnityEngine.Color.white;
        model.SetActive(selected);
    }
}
