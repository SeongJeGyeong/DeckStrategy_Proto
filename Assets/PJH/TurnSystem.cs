using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] slots;     // 슬롯 6개 (LineupSlot이 붙은 오브젝트)
    [SerializeField] private Button nextTurnButton;  // 버튼 (인스펙터 연결)
    [SerializeField] private TextMeshProUGUI roundText;         // UI 표시용
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

        // 슬롯에 캐릭터가 배치된 경우만 수집
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
            Debug.LogWarning(" 전투에 참여중인 캐릭터가 없습니다!");
            return;
        }

        // Speed 순으로 정렬 (내림차순)
        activeSlots.Sort((a, b) => b.model.characterBase.characterData.speed.CompareTo(a.model.characterBase.characterData.speed));

        foreach (var slot in activeSlots)
            turnOrder.Add(slot.model.characterBase);

        currentTurnIndex = 0;
        currentRound = 1;
        UpdateRoundUI();

        Debug.Log(" 턴 시스템 초기화 완료. 참가자 수: " + turnOrder.Count);
    }

    private void NextTurn()
    {
        if (turnOrder.Count == 0)
            return;

        CharacterBase currentCharacter = turnOrder[currentTurnIndex];
        Debug.Log($" Round {currentRound} | Turn {currentTurnIndex + 1} : {currentCharacter.characterData.name} 행동!");

        // 다음 턴 인덱스로
        currentTurnIndex++;

        // 모든 캐릭터가 턴을 마쳤으면 라운드 종료
        if (currentTurnIndex >= turnOrder.Count)
        {
            currentTurnIndex = 0;
            currentRound++;
            
            Debug.Log($" 라운드 {currentRound} 시작!");
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