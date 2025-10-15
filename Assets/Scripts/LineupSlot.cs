using UnityEngine;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    GameObject CharacterModelPrefab;
    Transform slotTransform;
    public bool isPlaced = false;

    GameObject model;

    void Start()
    {
        slotTransform = this.transform;
        model = Instantiate(CharacterModelPrefab, slotTransform);
        model.SetActive(false);
    }

    public void SetSelectedCharacter(Material material, bool selected)
    {
        isPlaced = selected;
        model.GetComponent<MeshRenderer>().material = material;
        model.SetActive(selected);
    }
}
