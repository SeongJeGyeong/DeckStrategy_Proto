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
        // 토글 버튼의 start가 LineupSlot의 Start보다 먼저 실행되어 모델을 생성하지 못할 수도 있기에 Awake에서 호출
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
