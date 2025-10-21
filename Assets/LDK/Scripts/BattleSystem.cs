using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Team team;
    public GameObject[] friendlySlots = new GameObject[5];
    public GameObject[] enemySlots = new GameObject[1];

    private Dictionary<int, Character> battleSequence = new Dictionary<int, Character>(); //���� int ���� ĳ���͸� �����صΰ�
    // map�� key�� �������� ������ �Ǵµ� DIctionary�� �׷��ٸ� foreach�� �Ѹ� ���� attack����

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
            //slot.model.characterBase.characterData.speed //speed �� ���� ��������..
        }
    }

    public void BattleStart()
    {

    }
}
