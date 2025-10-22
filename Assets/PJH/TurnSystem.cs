using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] slots;     // ���� 6�� (LineupSlot�� ���� ������Ʈ)
    [SerializeField] private Button nextTurnButton;  // ��ư (�ν����� ����)
    [SerializeField] private TextMeshProUGUI roundText;         // UI ǥ�ÿ�
    [SerializeField] private TextMeshProUGUI turnText;

    private List<LineupSlot> activeSlots = new List<LineupSlot>();
    private List<CharacterBase> turnOrder = new List<CharacterBase>();

    private int currentTurnIndex = 0;
    private int currentRound = 1;


    private void Start()
    {
        InitTurnSystem();

        if (nextTurnButton != null)
            nextTurnButton.onClick.AddListener(NextTurn);
    }

    public void InitTurnSystem()
    {
        activeSlots.Clear();
        turnOrder.Clear();

        // ���Կ� ĳ���Ͱ� ��ġ�� ��츸 ����
        foreach (var slotObj in slots)
        {
            LineupSlot slot = slotObj.GetComponent<LineupSlot>();
            if (slot != null && slot.isPlaced && slot.model.characterBase != null)
            {
                activeSlots.Add(slot);
            }
        }

        if (activeSlots.Count == 0)
        {
            Debug.LogWarning(" ������ �������� ĳ���Ͱ� �����ϴ�!");
            return;
        }

        // Speed ������ ���� (��������)
        activeSlots.Sort((a, b) => b.model.characterBase.characterData.speed.CompareTo(a.model.characterBase.characterData.speed));

        foreach (var slot in activeSlots)
            turnOrder.Add(slot.model.characterBase);

        currentTurnIndex = 0;
        currentRound = 1;
        UpdateRoundUI();

        Debug.Log(" �� �ý��� �ʱ�ȭ �Ϸ�. ������ ��: " + turnOrder.Count);
    }

    private void NextTurn()
    {
        if (turnOrder.Count == 0)
            return;

        CharacterBase currentCharacter = turnOrder[currentTurnIndex];
        Debug.Log($" Round {currentRound} | Turn {currentTurnIndex + 1} : {currentCharacter.characterData.name} �ൿ!");

        // ���� �� �ε�����
        currentTurnIndex++;

        // ��� ĳ���Ͱ� ���� �������� ���� ����
        if (currentTurnIndex >= turnOrder.Count)
        {
            currentTurnIndex = 0;
            currentRound++;
            
            Debug.Log($" ���� {currentRound} ����!");
        }
        UpdateRoundUI();
    }

    private void UpdateRoundUI()
    {
        if (roundText != null)
            roundText.text = $"Round {currentRound}";
        if (turnText != null)
            turnText.text = $"Turn {currentTurnIndex + 1}/{turnOrder.Count}";
    }
}