using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FormationSystem : MonoBehaviour
{
    public GameObject[] slots;

    [SerializeField]
    private ScrollView characterListView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int PlaceCharacter(Material material)
    {
        for(int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            if (!slot.isPlaced)
            {
                slot.SetSelectedCharacter(material, true);
                return i+1;
            }
        }

        return 0;
    }

    public void ReleaseCharacter(Material material, int slotNumber)
    {
        LineupSlot slot = slots[slotNumber-1].GetComponent<LineupSlot>();
        slot.SetSelectedCharacter(material, false);
    }
}
