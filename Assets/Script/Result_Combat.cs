using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class CharacterResult
{
    public Sprite image;
    public string name;
    public float contribution; // 실제 수치 (예: 2300)
}

public class Result_Combat : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image mvpBackground;
    [SerializeField] private Transform characterListParent;
    [SerializeField] private GameObject characterSlotPrefab;

    [Header("Test Data")]
    [SerializeField] private List<CharacterResult> results = new List<CharacterResult>();

    private void Start()
    {
        DisplayResults();
    }

    private void DisplayResults()
    {
        if (results == null || results.Count == 0) return;

        // 기여도 순 정렬
        var sorted = results.OrderByDescending(r => r.contribution).ToList();
        float mvpValue = sorted[0].contribution;

        // MVP 배경 설정
        if (mvpBackground && sorted[0].image)
        {
            mvpBackground.sprite = sorted[0].image;
            mvpBackground.color = new Color(1, 1, 1, 0.25f);
        }

        // Slot 생성
        foreach (var data in sorted)
        {
            GameObject slot = Instantiate(characterSlotPrefab, characterListParent);
            var ui = slot.GetComponent<CharacterSlotUI>();

            float percent = (data.contribution / mvpValue) * 100f;
            ui.SetData(data.image, data.name, percent);
        }
    }
}