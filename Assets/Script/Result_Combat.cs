using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class Result_Combat : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image mvpBackground;
    [SerializeField] private Transform characterListParent;
    [SerializeField] private GameObject characterSlotPrefab;

    [Header("Combat Data List (ScriptableObjects)")]
    [SerializeField] private List<CombatData> combatDatas = new List<CombatData>();

    private void Start()
    {
        DisplayResults();
    }

    private void DisplayResults()
    {
        if (combatDatas == null || combatDatas.Count == 0) return;

        // 기여도 순 정렬
        var sorted = combatDatas.OrderByDescending(c => c.Contribution).ToList();
        float mvpValue = sorted[0].Contribution;

        // MVP 배경 설정
        if (mvpBackground && sorted[0].Icon)
        {
            mvpBackground.sprite = sorted[0].Icon;
            mvpBackground.color = new Color(1, 1, 1, 0.25f);
        }

        // 슬롯 생성
        foreach (var data in sorted)
        {
            GameObject slot = Instantiate(characterSlotPrefab, characterListParent);
            var ui = slot.GetComponent<CharacterSlotUI>();
            float percent = (data.Contribution / mvpValue) * 100f;
            ui.SetData(data.Icon, data.CharacterName, percent);
        }
    }
}