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
            Debug.LogWarning($"{gameObject.name}: CharacterBase�� ����ֽ��ϴ�!");
            return;
        }

        var data = characterBase.characterData;
        var model = characterBase.characterModelData;

        // �̹��� ����
        if (image != null && model != null)
            image.material = model.material;

        // �̸� ����
        if (nameText != null && data != null)
            nameText.text = data.characterName;

        // ���ݷ�(�⿩��) ����
        if (contributionText != null && data != null)
            contributionText.text = $"���ݷ�: {data.attack}";
    }
}