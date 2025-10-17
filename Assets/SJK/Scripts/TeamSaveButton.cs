using UnityEngine;
using UnityEngine.UI;

public class TeamSaveButton : MonoBehaviour
{
    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private FormationSystem formationSystem;

    void Start()
    {
        saveButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        formationSystem.SaveTeam();
    }
}
