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
    [SerializeField] private float turnDelay = 1f; // 턴 진행 딜레이(디버그용)

    private List<Character> characters = new List<Character>();
    private int currentTurnIndex = 0;
    private int currentRound = 1;
    private bool isInitialized = false;

    // -----------------------------
    // 배치 완료 후 외부에서 호출
    public void InitTurnSystem()
    {
        if (isInitialized) return;

        characters.Clear();

        // 씬에서 활성화된 Character 모두 검색
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
            Debug.LogWarning("TurnSystem: 전투 참여 캐릭터가 없습니다.");
            return;
        }

        // Speed 기준 내림차순 정렬
        characters = characters.OrderByDescending(c => c.characterBase.characterData.speed).ToList();

        Debug.Log($"[TurnSystem] {characters.Count}명의 캐릭터가 전투에 참여합니다.");

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

        // 디버그 로그
        Debug.Log($"[Round {currentRound}] 턴 진행: {currentChar.characterBase.characterData.characterName}");

        // UI 업데이트
        if (turnText != null)
            turnText.text = $"현재 턴: {currentChar.characterBase.characterData.characterName}";

        // 테스트용: 지정된 딜레이 후 다음 턴
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

        // Speed 기준 재정렬 (스피드가 변할 수 있는 경우 대비)
        characters = characters.OrderByDescending(c => c.characterBase.characterData.speed).ToList();

        Debug.Log($"==== Round {currentRound} 시작 ====");

        UpdateUI();
        StartNextTurn();
    }

    private void UpdateUI()
    {
        if (roundText != null)
            roundText.text = $"Round {currentRound}";
    }
}