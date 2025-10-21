using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private TMP_Text turnText;

    [Header("Settings")]
    [SerializeField] private float turnDelay = 1f; // �� ���� ������(����׿�)

    private List<Character> characters = new List<Character>();
    private int currentTurnIndex = 0;
    private int currentRound = 1;
    private bool isInitialized = false;

    // -----------------------------
    // ��ġ �Ϸ� �� �ܺο��� ȣ��
    public void InitTurnSystem()
    {
        if (isInitialized) return;

        characters.Clear();

        // ������ Ȱ��ȭ�� Character ��� �˻�
        Character[] found = FindObjectsByType<Character>(FindObjectsSortMode.None);

        foreach (var c in found)
        {
            if (c.characterBase != null && c.gameObject.activeInHierarchy)
            {
                characters.Add(c);
            }
        }

        if (characters.Count == 0)
        {
            Debug.LogWarning("TurnSystem: ���� ���� ĳ���Ͱ� �����ϴ�.");
            return;
        }

        // Speed ���� �������� ����
        characters = characters.OrderByDescending(c => c.characterBase.characterData.speed).ToList();

        Debug.Log($"[TurnSystem] {characters.Count}���� ĳ���Ͱ� ������ �����մϴ�.");

        isInitialized = true;
        UpdateUI();
        StartNextTurn();
    }

    // -----------------------------
    private void StartNextTurn()
    {
        if (!isInitialized) return;

        if (currentTurnIndex >= characters.Count)
        {
            NextRound();
            return;
        }

        Character currentChar = characters[currentTurnIndex];

        // ����� �α�
        Debug.Log($"[Round {currentRound}] �� ����: {currentChar.characterBase.characterData.characterName}");

        // UI ������Ʈ
        if (turnText != null)
            turnText.text = $"���� ��: {currentChar.characterBase.characterData.characterName}";

        // �׽�Ʈ��: ������ ������ �� ���� ��
        Invoke(nameof(EndTurn), turnDelay);
    }

    private void EndTurn()
    {
        currentTurnIndex++;
        StartNextTurn();
    }

    private void NextRound()
    {
        currentRound++;
        currentTurnIndex = 0;

        // Speed ���� ������ (���ǵ尡 ���� �� �ִ� ��� ���)
        characters = characters.OrderByDescending(c => c.characterBase.characterData.speed).ToList();

        Debug.Log($"==== Round {currentRound} ���� ====");

        UpdateUI();
        StartNextTurn();
    }

    private void UpdateUI()
    {
        if (roundText != null)
            roundText.text = $"Round {currentRound}";
    }
}