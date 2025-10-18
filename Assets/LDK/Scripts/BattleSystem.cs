using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Team team;
    public GameObject[] slots = new GameObject[6];


    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            LineupSlot slot = slots[i].GetComponent<LineupSlot>();
            CharacterBase characterBase = team.characters[i];
            slot.SetSelectedCharacter(characterBase);
        }
    }
}
