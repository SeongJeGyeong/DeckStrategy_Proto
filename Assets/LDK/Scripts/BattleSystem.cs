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
    [SerializeField]
    public GameObject[] enemySlots = new GameObject[1];

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


    private void Start()
    {
        // �׽�Ʈ�� �� 1�� ����
        LineupSlot enemySlot = enemySlots[0].GetComponent<LineupSlot>();
        CharacterBase enemyBase = enemyTeam.characters[0];
        enemySlot.SetSelectedCharacter(enemyBase, true);
    }

    // ��Ʋ ���� ����
    private void SortBattleSequence()
    {
        battleSequence.Clear();
        sequenceImage.ForEach(Destroy);
        sequenceImage.Clear();

        // �Ʊ� ���
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            if (friendlySlot.model != null)
                battleSequence.Add(friendlySlot.model);
        }

        // �� ���
        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            if (enemySlot.model != null)
                battleSequence.Add(enemySlot.model);
        }

        // �ӵ��� ���� (��������)
        battleSequence.Sort((a, b) => b.characterBase.characterData.speed.CompareTo(a.characterBase.characterData.speed));

        // ���� ������ ǥ��
        foreach (var character in battleSequence)
        {
            var icon = Instantiate(iconPrefab, characterSequenceList);
            var portrait = icon.transform.Find("Portrait")?.GetComponent<Image>();
            if (portrait != null)
            {
                Color c = character.characterBase.characterModelData.material.GetColor("_BaseColor");
                portrait.color = c;
            }
            sequenceImage.Add(icon);
        }
    }

    public void Resort()
    {
        // ���� ������ ����
        foreach (var icon in sequenceImage)
            Destroy(icon);
        sequenceImage.Clear();

        // �ӵ��� ������
        battleSequence.Sort((x, y) => y.characterBase.characterData.speed.CompareTo(x.characterBase.characterData.speed));

        // �����
        foreach (var character in battleSequence)
        {
            var icon = Instantiate(iconPrefab, characterSequenceList);
            var portrait = icon.transform.Find("Portrait")?.GetComponent<Image>();
            if (portrait != null)
            {
                var color = character.characterBase.characterModelData.material.GetColor("_BaseColor");
                portrait.color = color;
            }
            sequenceImage.Add(icon);
        }
    }

    public void BattleStart()
    {
        isBattleStart = true;

        // �� ���� ����
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            var slot = friendlySlots[i].GetComponent<LineupSlot>();
            if (slot.model != null)
            {
                friendlyTeam.characters[i] = slot.model.characterBase;
                slot.ActivateBattleUI();
            }
        }

        for (int i = 0; i < enemySlots.Length; i++)
        {
            var slot = enemySlots[i].GetComponent<LineupSlot>();
            if (slot.model != null)
            {
                enemyTeam.characters[i] = slot.model.characterBase;
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

    // �� ���� ��ư
    public void NextTurn()
    {
        if (!isBattleStart || battleSequence.Count == 0)
            return;

        // �� ���尡 �����ٸ� �� �����
        if (currentTurnIndex >= battleSequence.Count)
        {
            currentRound++;
            // ���� �Ѿ�ڸ��� ������ �� UI �����
            Resort();
            // �ٽ� ù ��° ĳ���ͺ��� ����
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

        // ���� Ÿ�� ����
        if (currentChar.isEnemy)
        {
            int targetIndex = Random.Range(0, friendlySlots.Length);
            currentChar.AtackComp.targetIndex = targetIndex;
        }
        else
        {
            int targetIndex = Random.Range(0, enemySlots.Length);
            currentChar.AtackComp.targetIndex = targetIndex;
        }

        // �ൿ ����
        currentChar.AtackComp.Attack();
        Debug.Log($"[Round {currentRound}] Turn {currentTurnIndex + 1} �� {currentChar.characterBase.characterData.name} attacks!");

        //  �̹� �� ĳ���� ������ ����
        if (currentTurnIndex < sequenceImage.Count && sequenceImage[currentTurnIndex] != null)
        {
            sequenceImage[currentTurnIndex].SetActive(false);
        }

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
        Debug.Log($" Round {currentRound} ����!");

        Resort(); // �ӵ��� ����
        currentTurnIndex = 0;

        // �� ������� NextTurn ȣ��
        while (currentTurnIndex < battleSequence.Count)
        {
            NextTurn();

            // ������ ���� ������ ���
            Character c = battleSequence[currentTurnIndex - 1]; // NextTurn���� �ε��� ������
            if (c != null && c.AtackComp != null)
                yield return new WaitWhile(() => c.AtackComp.isAttacking);

            // �� ���� �� (�����)
            yield return new WaitForSeconds(0.2f);
        }

        // ���� ���� ó��
        currentRound++;
        currentTurnIndex = 0;
        UpdateUI();

        Resort();

        Debug.Log($" Round {currentRound - 1} ����");
    }

    private void UpdateUI()
    {
        if (roundText != null)
            roundText.text = $"Round {currentRound}";

        if (turnText != null)
        {
            if (currentTurnIndex < battleSequence.Count)
            {
                // ���� �� ĳ���� �̸� ǥ��
                var currentChar = battleSequence[currentTurnIndex];
                if (currentChar != null && currentChar.characterBase != null)
                    turnText.text = $"{currentChar.characterBase.characterData.characterName} Turn";
                else
                    turnText.text = $"---";
            }
            else
            {
                // ��� ���� ���� ���
                turnText.text = $"0 Turn";
            }
        }
    }
}