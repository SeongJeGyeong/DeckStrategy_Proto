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
        // 토글 버튼의 start가 LineupSlot의 Start보다 먼저 실행되어 모델을 생성하지 못할 수도 있기에 Awake에서 호출
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
