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
    private UserData.Team friendlyTeam = new UserData.Team();

    [SerializeField]
    private UserData.Team enemyTeam;

    public GameObject[] friendlySlots = new GameObject[5];
    public GameObject[] enemySlots = new GameObject[5];

    [SerializeField] private RectTransform characterSequenceList;
    private List<Character> battleSequence = new List<Character>();
    private List<GameObject> sequenceImage = new List<GameObject>();

    [SerializeField]
    private GameObject iconPrefab;

    [SerializeField]
    private GameObject battleCanvas;
    [SerializeField]
    private GameObject formationCanvas;

    Coroutine battleRoutin;

    public bool isBattleStart = false;

    private void Start()
    {
        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            if (enemyTeam.characters[i] == null) continue;
            CharacterBase enemyBase = enemyTeam.characters[i];
            enemySlot.SetSelectedCharacter(enemyBase, true);
        }
    }

    private void SortBattleSequence()
    {
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            CharacterBase freindlyBase = friendlyTeam.characters[i];
            battleSequence.Add(friendlySlot.model);
        }

        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            if (enemyTeam.characters[i] == null) continue;
            CharacterBase enemyBase = enemyTeam.characters[i];
            battleSequence.Add(enemySlot.model);
        }

        for (int i = 0; i < battleSequence.Count; i++)
        {
            var Icon = Instantiate(iconPrefab, characterSequenceList);
            var portrait = Icon.transform.Find("Portrait")?.GetComponent<UnityEngine.UI.Image>();
            var color = battleSequence[i].characterBase.characterModelData.material.GetColor("_BaseColor");
            portrait.color = color;
            sequenceImage.Add(Icon);
        }
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
            var Icon = Instantiate(iconPrefab, characterSequenceList);
            var portrait = Icon.transform.Find("Portrait")?.GetComponent<UnityEngine.UI.Image>();
            var color = battleSequence[i].characterBase.characterModelData.material.GetColor("_BaseColor");
            portrait.color = color;
            sequenceImage.Add(Icon);
        }
    }
    public void BattleStart()
    {
        isBattleStart = true;
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            friendlyTeam.characters[i] = friendlySlot.model.characterBase;
            friendlySlot.ActivateBattleUI();
        }
        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            enemyTeam.characters[i] = enemySlot.model.characterBase;
            enemySlot.ActivateBattleUI();
        }

        formationCanvas.SetActive(false);
        battleCanvas.SetActive(true);
    }

    public void StartBattleSequence()
    {
        if (battleRoutin != null)
            StopCoroutine(battleRoutin);

        SortBattleSequence();
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
                int random = UnityEngine.Random.Range(0, enemySlots.Length - 1);
                character.AtackComp.targetIndex = random;
            }
            character.AtackComp.Attack();

            yield return new WaitWhile(() => character.AtackComp != null && character.AtackComp.isAttacking);

            if (characterSequenceList.childCount > 0)
            {
                Transform first = characterSequenceList.GetChild(0);
                Destroy(first.gameObject);
            }
        }
        battleSequence.Clear();
        battleRoutin = null;
    }
}
