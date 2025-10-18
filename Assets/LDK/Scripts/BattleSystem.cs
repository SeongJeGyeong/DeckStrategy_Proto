using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Team team;
    public GameObject[] slots = new GameObject[6];

    private Dictionary<int, Character> battleSequence = new Dictionary<int, Character>(); //���� int ���� ĳ���͸� �����صΰ�
    // map�� key�� �������� ������ �Ǵµ� DIctionary�� �׷��ٸ� foreach�� �Ѹ� ���� attack����

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
            //slot.model.characterBase.characterData.speed //speed �� ���� ��������..
        }
    }
}
