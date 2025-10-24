using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static Result_CharacterDisplay;
using static UnityEngine.GraphicsBuffer;

public class FormationSystem : MonoBehaviour
{
    [SerializeField]
    DataCenter dataCenter;

    [SerializeField]
    private TeamDataTable teamDataTable;

    public GameObject[] slots = new GameObject[5];
    //private UserData.Team[] teams = new UserData.Team[8];

    [SerializeField]
    private Transform characterListContent;

    public int selectedTeamIndex = 0;
    public int selectedCount = 0;

    public delegate void CharacterPlacedHandler(int slotIndex, CharacterData characterBase);
    public delegate void CharacterReleasedHandler(int slotIndex);

    public CharacterPlacedHandler placedHandler;
    public CharacterReleasedHandler releaseHandler;

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

        OwnedCharacterInfo[] characters = teamDataTable.teams[teamIndex].characters;
        for (int i = 0; i < slots.Length; ++i)
        {
            if (characters[i].characterID == 0) return;
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            slot.SetSelectedCharacter(characters[i], false);
            ++selectedCount;
            CharacterIcon[] icons = characterListContent.GetComponentsInChildren<CharacterIcon>();
            foreach (var icon in icons)
            {
                if (icon.characterInfo.characterID == characters[i].characterID)
                {
                    icon.selectedButton.ButtonClicked();
                    break;
                }
            }
        }
    }

    public void PlaceCharacter(OwnedCharacterInfo info)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            if (!slot.isPlaced)
            {
                slot.SetSelectedCharacter(info, false);
                ++selectedCount;
                //placedHandler?.Invoke(i, characterBase);
                return;
            }
        }
    }

    // �̹� ���õ� ĳ���͸� ������ ��
    public void ReleaseCharacter(int characterID)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            // ĳ���Ͱ� ������ ���� ������ �ѱ�
            if (slot.character == null) continue;
            if (slot.character.characterData.ID == characterID)
            {
                slot.DeselectCharacter();
                --selectedCount;
                //releaseHandler?.Invoke(i);
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
            teamDataTable.teams[selectedTeamIndex].characters[i] = slot.characterInfo;
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(teamDataTable);
        AssetDatabase.SaveAssets();
#endif
    }
}
