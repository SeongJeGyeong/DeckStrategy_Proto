using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Team team;
    public GameObject[] friendlySlots = new GameObject[5];
    public GameObject[] enemySlots = new GameObject[1];

    private Dictionary<int, Character> battleSequence = new Dictionary<int, Character>(); //순서 int 별로 캐릭터를 저장해두고
    // map은 key를 바탕으로 정렬이 되는데 DIctionary도 그렇다면 foreach로 한명씩 빼서 attack수행

    private void Start()
    {
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            CharacterBase freindlyBase = team.characters[i];
            friendlySlot.SetSelectedCharacter(freindlyBase, false);
        }

        LineupSlot enemySlot = enemySlots[0].GetComponent<LineupSlot>();
        CharacterBase enemyBase = team.characters[5];
        enemySlot.SetSelectedCharacter(enemyBase, true);
    }

    private void SetCharacterSeqeunce()
    {
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot slot = friendlySlots[i].GetComponent<LineupSlot>();
            //slot.model.characterBase.characterData.speed //speed 값 빼서 어케하지..
        }
    }

    public void BattleStart()
    {

    }
}
