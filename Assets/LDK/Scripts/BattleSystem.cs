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


    private void Start()
    {
        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            if (enemyTeam.characters[i] == null) continue;
            enemySlot.SetSelectedCharacter(enemyTeam.characters[i], true);
        }
    }

    // 배틀 순서 정렬
    private void SortBattleSequence()
    {
        battleSequence.Clear();
        sequenceImage.ForEach(Destroy);
        sequenceImage.Clear();

        // 아군 등록
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            LineupSlot friendlySlot = friendlySlots[i].GetComponent<LineupSlot>();
            if (friendlySlot.character != null)
                battleSequence.Add(friendlySlot.character);
        }

        for (int i = 0; i < enemySlots.Length; i++)
        {
            LineupSlot enemySlot = enemySlots[i].GetComponent<LineupSlot>();
            if (enemyTeam.characters[i] == null) continue;
            battleSequence.Add(enemySlot.character);
        }

        // 속도순 정렬 (내림차순)
        battleSequence.Sort((a, b) => b.characterData.speed.CompareTo(a.characterData.speed));

        // 순서 아이콘 표시
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
        // 기존 아이콘 제거
        foreach (var icon in sequenceImage)
            Destroy(icon);
        sequenceImage.Clear();

        // 속도순 재정렬
        battleSequence.Sort((x, y) => y.characterData.speed.CompareTo(x.characterData.speed));

        // 재생성
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

        // 팀 정보 저장
        for (int i = 0; i < friendlySlots.Length; i++)
        {
            var slot = friendlySlots[i].GetComponent<LineupSlot>();
            if (slot.character != null)
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

    // 턴 진행 버튼
    public void NextTurn()
    {
        if (!isBattleStart || battleSequence.Count == 0)
            return;

        // 한 라운드가 끝났다면 새 라운드로
        if (currentTurnIndex >= battleSequence.Count)
        {
            currentRound++;
            // 라운드 넘어가자마자 재정렬 및 UI 재생성
            Resort();
            // 다시 첫 번째 캐릭터부터 시작
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
        // 공격 타겟 설정
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

        // 행동 실행
        currentChar.AtackComp.Attack();

        //  이번 턴 캐릭터 아이콘 제거
        if (currentTurnIndex < sequenceImage.Count && sequenceImage[currentTurnIndex] != null)
        {
            sequenceImage[currentTurnIndex].SetActive(false);
        }

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
                // 현재 턴 캐릭터 이름 표시
                var currentChar = battleSequence[currentTurnIndex];
                if (currentChar != null && currentChar.characterData != null)
                    turnText.text = $"{currentChar.characterData.characterName} Turn";
                else
                    turnText.text = $"---";
            }
            else
            {
                // 모든 턴이 끝난 경우
                turnText.text = $"";
            }
        }
    }

    private IEnumerator WaitForAttackEnd(Character currentChar)
    {
        yield return new WaitWhile(() => currentChar.AtackComp.isAttacking);

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
        Debug.Log($" Round {currentRound} 시작!");

        Resort(); // 속도순 정렬
        currentTurnIndex = 0;

        // 턴 순서대로 NextTurn 호출
        while (currentTurnIndex < battleSequence.Count)
        {
            NextTurn();

            // 공격이 끝날 때까지 대기
            Character c = battleSequence[currentTurnIndex - 1]; // NextTurn에서 인덱스 증가함
            if (c != null && c.AtackComp != null)
                yield return new WaitWhile(() => c.AtackComp.isAttacking);

            // 턴 사이 텀 (연출용)
            yield return new WaitForSeconds(0.2f);
        }

        // 라운드 종료 처리
        currentRound++;
        currentTurnIndex = 0;
        UpdateUI();

        Resort();

        Debug.Log($" Round {currentRound - 1} 종료");
    }
}