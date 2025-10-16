using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlotUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Slider contributionBar;

    [Header("Settings")]
    [SerializeField] private int maxContribution;

    [Header("Data")]
    [SerializeField] private CombatData combatData;

    private void Start()
    {
        if (combatData != null)
        {
            SetData(combatData.Icon, combatData.name, combatData.Contribution);
        }
    }

    public void SetData(Sprite img, string charName, float contributionPercent)
    {
        if (icon) icon.sprite = img;
        if (nameText) nameText.text = charName;
        if (contributionBar) contributionBar.value = contributionPercent;
    }


}