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

    [SerializeField]
    protected SelectedButton selectedButton;

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
        //portrait.material = characterBase.characterModelData.material;
    }

    private void OnButtonClicked()
    {
        if(selectedButton.isSelected)
        {
            selectedButton.ButtonClicked();
            formationSystem.ReleaseCharacter(portrait.material, slotNumber);
            slotNumber = 0;
            return;
        }

        slotNumber = formationSystem.PlaceCharacter(portrait.material);
        if (slotNumber != 0)
        {
            selectedButton.ButtonClicked();
        }
    }
}
