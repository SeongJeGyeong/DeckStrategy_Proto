using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Team team;
    public GameObject[] slots = new GameObject[6];

    private Dictionary<int, Character> battleSequence = new Dictionary<int, Character>(); //순서 int 별로 캐릭터를 저장해두고
    // map은 key를 바탕으로 정렬이 되는데 DIctionary도 그렇다면 foreach로 한명씩 빼서 attack수행

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            CharacterBase characterBase = team.characters[i];
            slot.SetSelectedCharacter(characterBase);
        }
    }

    private void SetCharacterSeqeunce()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            //slot.model.characterBase.characterData.speed //speed 값 빼서 어케하지..
        }
    }
}
