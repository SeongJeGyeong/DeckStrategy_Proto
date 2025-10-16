using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FormationSystem : MonoBehaviour
{
    public GameObject[] slots = new GameObject[5];
    private Team[] teams = new Team[8];

    private int selectedTeamIndex = 0;

    [SerializeField]
    private Transform characterListContent;

    void Start()
    {

    }

    public int PlaceCharacter(Color color)
    {
        for(int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            if (!slot.isPlaced)
            {
                slot.SetSelectedCharacter(color, true);
                return i+1;
            }
        }

        return 0;
    }

    public void PlaceTeam(int teamIndex)
    {
        selectedTeamIndex = teamIndex;
        if (teams[teamIndex] == null)
        {
            for (int i = 0; i < slots.Length; ++i)
            {
                ReleaseCharacter(Color.white, i+1);
            }
            ResetCharacterList();
            return;
        }

        CharacterBase[] characters = teams[teamIndex].characters;
        for (int i = 0; i < slots.Length; ++i)
        {
            if(characters[i] == null)
            {
                ReleaseCharacter(Color.white, i+1);
            }
            else
            {
                LineupSlot slot = slots[i].GetComponent<LineupSlot>();
                slot.SetSelectedCharacter(characters[i].characterModelData.material.color, true);
            }
        }

        ResetCharacterList();
    }

    public void ReleaseCharacter(Color color, int slotNumber)
    {
        LineupSlot slot = slots[slotNumber-1].GetComponent<LineupSlot>();
        slot.SetSelectedCharacter(color, false);
    }

    private void ResetCharacterList()
    {
        CharacterIcon[] icons = characterListContent.GetComponentsInChildren<CharacterIcon>();
        foreach (var icon in icons)
        {
            if (icon.selectedButton.isSelected)
            {
                icon.selectedButton.ButtonClicked();
            }
        }
    }

    public void SaveTeam(int teamIndex)
    {
        for (int i = 0; i < slots.Length; ++i)
        {

        }
    }
}
