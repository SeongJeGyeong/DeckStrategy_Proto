using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;


public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    private DataCenter dataCenter;

    private UserData.Team friendlyTeam = new UserData.Team();

    [SerializeField]
    private UserData.Team enemyTeam;
    [SerializeField]
    public GameObject[] friendlySlots = new GameObject[5];
    public GameObject[] enemySlots = new GameObject[5];

    [SerializeField]
    private RectTransform characterSequenceList;
    [SerializeField]
    private GameObject iconPrefab;
    [SerializeField]
    private GameObject battleCanvas;
    [SerializeField]
    private GameObject formationCanvas;

    [SerializeField]
    private TextMeshProUGUI roundText;
    [SerializeField]
    private TextMeshProUGUI turnText;

    [SerializeField]
    private TextMeshProUGUI playerCP;
    [SerializeField]
    private TextMeshProUGUI enemyCP;

    private List<Character> battleSequence = new List<Character>();
    private List<GameObject> sequenceImage = new List<GameObject>();

    private IAttackTargetSelector targetSelector;

    private int currentTurnIndex = 0;
    private int currentRound = 1;

    private bool isBattleStart = false;

    private void Start()
    {
        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            enemySlot.OnCPUpdated += UpdateEnemyCP;
            if (enemyTeam.characters[i] == null) continue;

            enemySlot.SetSelectedCharacter(enemyTeam.characters[i], true);
        }

        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            friendlySlot.OnCPUpdated += UpdatePlayerCP;
        }

        targetSelector = new ChainedTargetSelector(
           new PickWeakTarget(this),
           new PickRandomTarget(this)
       );
    }

    private void SortBattleSequence()
    {
        battleSequence.Clear();
        sequenceImage.ForEach(Destroy);
        sequenceImage.Clear();

        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            if (friendlySlot.character != null && friendlySlot.isPlaced)
                battleSequence.Add(friendlySlot.character);
        }

        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            if (enemyTeam.characters[i] == null) continue;
            battleSequence.Add(enemySlot.character);
        }

        battleSequence.Sort((a, b) => b.characterData.speed.CompareTo(a.characterData.speed));

        foreach (var character in battleSequence)
        {
            var icon = Instantiate(iconPrefab, characterSequenceList);
            var portrait = icon.transform.Find("Portrait")?.GetComponent<Image>();
            if (portrait != null)
            {
                Color c = character.characterModelData.material.GetColor("_BaseColor");
                portrait.color = c;
            }
            sequenceImage.Add(icon);
        }
    }

    public void Resort()
    {
        foreach (var icon in sequenceImage)
            Destroy(icon);
        sequenceImage.Clear();

        battleSequence.Sort((x, y) => y.characterData.speed.CompareTo(x.characterData.speed));

        foreach (var character in battleSequence)
        {
            var icon = Instantiate(iconPrefab, characterSequenceList);
            var portrait = icon.transform.Find("Portrait")?.GetComponent<Image>();
            if (portrait != null)
            {
                var color = character.characterModelData.material.GetColor("_BaseColor");
                portrait.color = color;
            }
            sequenceImage.Add(icon);
        }
    }

    public void BattleStart()
    {
        isBattleStart = true;

        for (int i = 0; i < friendlySlots.Length; i++)
        {
            var slot = friendlySlots[i].GetComponent<LineupSlot>();
            if (slot.character != null && slot.isPlaced)
            {
                friendlyTeam.characters[i] = slot.character.characterInfo;
                slot.ActivateBattleUI();
            }
        }

        for (int i = 0; i < enemySlots.Length; i++)
        {
            var slot = enemySlots[i].GetComponent<LineupSlot>();
            if (slot.character.characterInfo != null)
            {
                enemyTeam.characters[i] = slot.character.characterInfo;
                slot.ActivateBattleUI();
            }
        }

        formationCanvas.SetActive(false);
        battleCanvas.SetActive(true);

        SortBattleSequence();

        currentTurnIndex = 0;
        currentRound = 1;
        UpdateUI();
    }

    public void NextTurn()
    {
        if (!isBattleStart || battleSequence.Count == 0)
            return;


        if (currentTurnIndex >= battleSequence.Count)
        {
            currentRound++;

            Resort();

            currentTurnIndex = 0;

            UpdateUI();
            return;
        }

        Character currentChar = battleSequence[currentTurnIndex];
        if (currentChar == null || currentChar.AtackComp == null)
        {
            currentTurnIndex++;
            return;
        }
        if (turnText != null)
            turnText.text = $"{currentChar.characterData.characterName} Turn";

        LineupSlot targetslot = targetSelector.SelectTarget(currentChar);
        currentChar.AtackComp.Attack(targetslot);

        StartCoroutine(WaitForAttackEnd(currentChar));
    }
    private void UpdateUI()
    {
        if (roundText != null)
            roundText.text = $"Round {currentRound}";

        if (turnText != null)
        {
            if (currentTurnIndex < battleSequence.Count)
            {
                var currentChar = battleSequence[currentTurnIndex];
                if (currentChar != null && currentChar.characterData != null)
                    turnText.text = $"{currentChar.characterData.characterName} Turn";
                else
                    turnText.text = $"---";
            }
            else
            {
                turnText.text = $"";
            }
        }
    }

    private IEnumerator WaitForAttackEnd(Character currentChar)
    {
        yield return new WaitWhile(() => currentChar.AtackComp.isAttacking);

        if (currentTurnIndex < sequenceImage.Count && sequenceImage[currentTurnIndex] != null)
        {
            sequenceImage[currentTurnIndex].SetActive(false);
        }

        Debug.Log("WaitForAttackEnd");
        currentTurnIndex++;
        UpdateUI();

    }
    public void NextRound()
    {
        if (!isBattleStart || battleSequence.Count == 0)
            return;

        StartCoroutine(CoNextRound());
    }

    private IEnumerator CoNextRound()
    {
        Debug.Log($" Round {currentRound} !");

        Resort();
        currentTurnIndex = 0;

        while (currentTurnIndex < battleSequence.Count)
        {
            NextTurn();
            var currentChar = battleSequence[currentTurnIndex];

            if (currentChar.characterData.rangeType == ERangeType.Melee)
            {
                yield return new WaitForSeconds(1.5f);
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
            }
        }

        currentRound++;
        currentTurnIndex = 0;
        UpdateUI();

        Resort();

        Debug.Log($" Round {currentRound - 1} Á¾·á");
    }

    void UpdatePlayerCP()
    {
        float cp = 0;
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot slot = friendlySlots[i].GetComponent<LineupSlot>();
            if (slot.character == null || slot.character.characterData == null) continue;
            cp += slot.character.characterData.maxHp +
                slot.character.characterData.attack +
                slot.character.characterData.defense +
                slot.character.characterData.speed;
        }

        playerCP.text = cp.ToString();
    }

    void UpdateEnemyCP()
    {
        float cp = 0;
        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot slot = enemySlots[i].GetComponent<LineupSlot>();
            if (slot.character == null || slot.character.characterData == null) continue;
            cp += slot.character.characterData.maxHp +
                slot.character.characterData.attack +
                slot.character.characterData.defense +
                slot.character.characterData.speed;
        }

        enemyCP.text = cp.ToString();
    }
}