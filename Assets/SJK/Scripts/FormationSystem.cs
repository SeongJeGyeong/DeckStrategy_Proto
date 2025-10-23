using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class FormationSystem : MonoBehaviour
{
    [SerializeField]
    private TeamDataTable teamDataTable;

    public GameObject[] slots = new GameObject[5];
    //private UserData.Team[] teams = new UserData.Team[8];

    public int selectedTeamIndex = 0;
    public int selectedCount = 0;

    [SerializeField]
    private Transform characterListContent;

    void Start()
    {

    }

    public void PlaceTeam(int teamIndex)
    {
        selectedTeamIndex = teamIndex;
        ResetCharacterList();

        for (int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            slot.DeselectCharacter();
        }
        selectedCount = 0;
        if (teamDataTable.teams[teamIndex] == null) return;

        CharacterBase[] characters = teamDataTable.teams[teamIndex].characters;
        for (int i = 0; i < slots.Length; ++i)
        {
            if (characters[i] == null) return;
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            slot.SetSelectedCharacter(characters[i], false);
            ++selectedCount;
            CharacterIcon[] icons = characterListContent.GetComponentsInChildren<CharacterIcon>();
            foreach (var icon in icons)
            {
                if (icon.characterBase.characterData.ID == slot.model.characterBase.characterData.ID)
                {
                    Debug.Log("PlaceID: " + slot.model.characterBase.characterData.ID);
                    icon.selectedButton.ButtonClicked();
                    break;
                }
            }
        }
    }

    public void PlaceCharacter(CharacterBase characterBase)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            if (!slot.isPlaced)
            {
                slot.SetSelectedCharacter(characterBase, false);
                ++selectedCount;
                return;
            }
        }
    }

    // 이미 선택된 캐릭터를 해제할 때
    public void ReleaseCharacter(int characterID)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            // 캐릭터가 편성되지 않은 슬롯은 넘김
            if (slot.model.characterBase == null) continue;
            if (slot.model.characterBase.characterData.ID == characterID)
            {
                slot.DeselectCharacter();
                --selectedCount;
                return;
            }
        }
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
            if (teamDataTable.teams[selectedTeamIndex] == null) teamDataTable.teams[selectedTeamIndex] = new UserData.Team();
            teamDataTable.teams[selectedTeamIndex].characters[i] = slot.model.characterBase;
        }
    }
}
