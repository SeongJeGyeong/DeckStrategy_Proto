using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Team team;
    public GameObject[] friendlySlots = new GameObject[5];
    public GameObject[] enemySlots = new GameObject[1];

    [SerializeField] private RectTransform panel;
    private List<Character> battleSequence = new List<Character>();
    private List<GameObject> sequenceImage = new List<GameObject>();

    [SerializeField]
    private GameObject iconPrefab;

    Coroutine battleRoutin;
    private void Start()
    {
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            CharacterBase freindlyBase = team.characters[i];
            friendlySlot.SetSelectedCharacter(freindlyBase, false);
            battleSequence.Add(friendlySlot.model);
        }

        LineupSlot enemySlot = enemySlots[0].GetComponent<LineupSlot>();
        CharacterBase enemyBase = team.characters[5];
        enemySlot.SetSelectedCharacter(enemyBase, true);
        battleSequence.Add(enemySlot.model);
    }

    public void Resort()
    {
        for(int i = 0; i < sequenceImage.Count; i++)
        {
            Destroy(sequenceImage[i]);
        }

        battleSequence.Sort((x, y) => x.characterBase.characterData.speed.CompareTo(y.characterBase.characterData.speed));
        battleSequence.Reverse();

        for (int i = 0; i < battleSequence.Count; i++)
        {
            var Icon = Instantiate(iconPrefab, panel);
            var portrait = Icon.transform.Find("Portrait")?.GetComponent<UnityEngine.UI.Image>();
            var color = battleSequence[i].characterBase.characterModelData.material.GetColor("_BaseColor");
            portrait.color = color;
            sequenceImage.Add(Icon);
        }
    }
    public void BattleStart()
    {
        if (battleRoutin != null)
            StopCoroutine(battleRoutin);

        battleRoutin = StartCoroutine(CoBattle());
    }

    IEnumerator CoBattle()
    {
        for (int i = 0; i < battleSequence.Count; i++)
        {
            Character character = battleSequence[i];
            if (battleSequence[i].isEnemy)
            {
                int random = UnityEngine.Random.Range(0, friendlySlots.Length - 1);
                character.AtackComp.targetIndex = random;
            }
            else
            {
                int random = enemySlots.Length - 1;
                character.AtackComp.targetIndex = random;
            }
            character.AtackComp.Attack();

            yield return new WaitWhile(() => character.AtackComp != null && character.AtackComp.isAttacking);
        }
        battleRoutin = null;
    }
}
