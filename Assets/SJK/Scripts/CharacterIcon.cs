using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils.Enums;

public class CharacterIcon : MonoBehaviour
{
    [SerializeField]
    private Image portrait;
    [SerializeField]
    private Image attributeIcon;
    [SerializeField]
    private TextMeshProUGUI level;

    public SelectedButton selectedButton;

    private FormationSystem formationSystem;

    public OwnedCharacterInfo characterInfo;

    private void Awake()
    {
        formationSystem = FindAnyObjectByType<FormationSystem>();
    }

    private void Start()
    {
        selectedButton.button.onClick.AddListener(OnButtonClicked);
    }

    public void SetData(CharacterData statusData, CharacterModelData modelData, int characterLevel)
    {
        level.text = "Lv." + characterLevel.ToString();

        portrait.color = modelData.material.color;

        if(statusData.type == EAttributeType.ROCK)
        {
            attributeIcon.color = Color.red;
        }
        else if(statusData.type == EAttributeType.SCISSORS)
        {
            attributeIcon.color = Color.green;
        }
        else
        {
            attributeIcon.color = Color.blue;
        }

        characterInfo.characterID = statusData.ID;
        characterInfo.characterModelID = modelData.ID;
        characterInfo.characterLevel = characterLevel;
    }

    private void OnButtonClicked()
    {
        if (selectedButton.isSelected)
        {
            if(formationSystem.selectedCount > 0)
            {
                formationSystem.ReleaseCharacter(characterInfo.characterID);
                selectedButton.ButtonClicked();
            }
        }
        else if(formationSystem.selectedCount < 5)
        {
            formationSystem.PlaceCharacter(characterInfo);
            selectedButton.ButtonClicked();
        }
    }
}
