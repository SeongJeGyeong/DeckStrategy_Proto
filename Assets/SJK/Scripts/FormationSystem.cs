using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FormationSystem : MonoBehaviour
{
    public GameObject[] slots = new GameObject[5];
    private Team[] teams = new Team[8];

    public int selectedTeamIndex = 0;

    [SerializeField]
    private Transform characterListContent;

    void Start()
    {

    }

    public int PlaceCharacter(CharacterBase characterBase)
    {
        for(int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            if (!slot.isPlaced)
            {
                slot.SetSelectedCharacter(characterBase);
                return i+1;
            }
        }

        return 0;
    }

    public void PlaceTeam(int teamIndex)
    {
        selectedTeamIndex = teamIndex;
        ResetCharacterList();

        if (teams[teamIndex] == null)
        {
            for (int i = 0; i < slots.Length; ++i)
            {
                ReleaseCharacter(i+1);
            }
            return;
        }

        CharacterBase[] characters = teams[teamIndex].characters;
        for (int i = 0; i < slots.Length; ++i)
        {
            if(characters[i] == null)
            {
                ReleaseCharacter(i+1);
            }
            else
            {
                LineupSlot slot = slots[i].GetComponent<LineupSlot>();
                slot.SetSelectedCharacter(characters[i]);
                CharacterIcon[] icons = characterListContent.GetComponentsInChildren<CharacterIcon>();
                foreach (var icon in icons)
                {
                    if (icon.characterBase.characterData.ID == slot.characterBase.characterData.ID)
                    {
                        icon.selectedButton.ButtonClicked();
                        break;
                    }
                }
            }
        }
    }

    public void ReleaseCharacter(int slotNumber)
    {
        LineupSlot slot = slots[slotNumber-1].GetComponent<LineupSlot>();
        slot.DeselectCharacter();
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

    public void SaveTeam()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            if (teams[selectedTeamIndex] == null) teams[selectedTeamIndex] = new Team();
            teams[selectedTeamIndex].characters[i] = slot.characterBase;
        }
    }
}
