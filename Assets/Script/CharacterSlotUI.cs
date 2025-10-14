using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Slider contributionBar;
    [SerializeField] private TextMeshProUGUI percentText;

    public void SetData(Sprite img, string name, float percent)
    {
        if (icon) icon.sprite = img;
        if (nameText) nameText.text = name;
        if (contributionBar) contributionBar.value = percent / 100f;
    }
}