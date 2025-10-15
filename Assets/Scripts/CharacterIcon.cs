using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : MonoBehaviour
{
    [SerializeField]
    private Image portrait;
    [SerializeField]
    private Image attributeIcon;
    [SerializeField]
    private TextMeshProUGUI level;

    private int characterId;

    public SelectedButton selectedButton;

    private FormationSystem formationSystem;

    private int slotNumber;

    private void Awake()
    {
        formationSystem = FindAnyObjectByType<FormationSystem>();
        selectedButton.button.onClick.AddListener(OnButtonClicked);
    }

    public void SetData(CharacterBase characterBase)
    {
        level.text = "Lv." + characterBase.Level.ToString();
        portrait.color = characterBase.characterModelData.material.color;

        if(characterBase.characterData.type == AttributeType.ROCK)
        {
            attributeIcon.color = Color.red;
        }
        else if(characterBase.characterData.type == AttributeType.SCISSORS)
        {
            attributeIcon.color = Color.green;
        }
        else
        {
            attributeIcon.color = Color.blue;
        }

        characterId = characterBase.characterData.ID;
    }

    private void OnButtonClicked()
    {
        if(selectedButton.isSelected)
        {
            selectedButton.ButtonClicked();
            formationSystem.ReleaseCharacter(portrait.color, slotNumber);
            slotNumber = 0;
            return;
        }

        slotNumber = formationSystem.PlaceCharacter(portrait.color);
        if (slotNumber != 0)
        {
            selectedButton.ButtonClicked();
        }
    }
}
