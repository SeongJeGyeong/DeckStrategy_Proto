using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlotUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text contributionText;

    public void SetData(CharacterBase characterBase)
    {
        if (characterBase == null)
        {
            Debug.LogWarning($"{gameObject.name}: CharacterBase가 비어있습니다!");
            return;
        }

        var data = characterBase.characterData;
        var model = characterBase.characterModelData;

        // 이미지 적용
        if (image != null && model != null)
            image.material = model.material;

        // 이름 적용
        if (nameText != null && data != null)
            nameText.text = data.characterName;

        // 공격력(기여도) 적용
        if (contributionText != null && data != null)
            contributionText.text = $"공격력: {data.attack}";
    }
}