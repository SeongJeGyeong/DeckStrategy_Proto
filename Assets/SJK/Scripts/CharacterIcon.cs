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

    public CharacterBase characterBase { get; private set; }

    public SelectedButton selectedButton;

    private FormationSystem formationSystem;

    private int slotNumber;

    private void Awake()
    {
        formationSystem = FindAnyObjectByType<FormationSystem>();
        selectedButton.button.onClick.AddListener(OnButtonClicked);
    }

    public void SetData(CharacterBase charBase)
    {
        characterBase = charBase;

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
    }

    private void OnButtonClicked()
    {
        if(selectedButton.isSelected)
        {
            selectedButton.ButtonClicked();
            formationSystem.ReleaseCharacter(slotNumber);
            slotNumber = 0;
            return;
        }

        slotNumber = formationSystem.PlaceCharacter(characterBase);
        if (slotNumber != 0)
        {
            selectedButton.ButtonClicked();
        }
    }
}
