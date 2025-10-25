using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BattleSystem : MonoBehaviour
{
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

    private List<Character> battleSequence = new List<Character>();
    private List<GameObject> sequenceImage = new List<GameObject>();

    private int currentTurnIndex = 0;
    private int currentRound = 1;

    private bool isBattleStart = false;

    private readonly int[] front = { 4, 5 };
    private readonly int[] back = { 1, 2, 3 };

    //FormationSystem formationSystem;

    private void Start()
    {
        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            if (enemyTeam.characters[i] == null) continue;
            enemySlot.SetSelectedCharacter(enemyTeam.characters[i], true);
        }
    }

    //private void AddSlot(int slotindex, CharacterBase characterBase)
    //{
    //    friendlyTeam.characters[slotindex] = characterBase; //SetSelectedCharacter(characterBase, false);
    //}

    //private void ReleaseSlot(int slotindex)
    //{
    //    friendlySlots[slotindex].DeselectCharacter();
    //}

    private void SortBattleSequence()
    {
        battleSequence.Clear();
        sequenceImage.ForEach(Destroy);
        sequenceImage.Clear();

        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            if (friendlySlot.character.characterData != null && friendlySlot.isPlaced)
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
            if (slot.character.characterData != null && slot.isPlaced)
            {
                friendlyTeam.characters[i] = slot.characterInfo;
                slot.ActivateBattleUI();
            }
        }

        for (int i = 0; i < enemySlots.Length; i++)
        {
            var slot = enemySlots[i].GetComponent<LineupSlot>();
            if (slot.characterInfo != null)
            {
                enemyTeam.characters[i] = slot.characterInfo;
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

       //공격로직
        
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

            yield return new WaitForSeconds(2.5f);
        }

        currentRound++;
        currentTurnIndex = 0;
        UpdateUI();

        Debug.Log($" Round {currentRound - 1} 종료");

        //라운드가 끝나면 아이콘 재생성 (SequenceList 복원)
        Resort();

        Debug.Log($" Round {currentRound} 준비 완료");
    }

}