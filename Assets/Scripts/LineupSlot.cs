using UnityEngine;

public class LineupSlot : MonoBehaviour
{
    [SerializeField]
    GameObject CharacterModelPrefab;
    Transform slotTransform;
    public bool isPlaced = false;

    void Start()
    {
        CharacterModelPrefab.SetActive(false);
    }

    public void SetSelectedCharacter(Color color, bool selected)
    {
        isPlaced = selected;
        CharacterModelPrefab.GetComponent<MeshRenderer>().material.color = color;
        CharacterModelPrefab.SetActive(selected);
    }
}
