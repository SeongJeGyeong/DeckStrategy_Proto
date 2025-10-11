using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private bool isSelected = false;
    private float targetAlpha;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Debug.Log("clicked");
        isSelected = !isSelected;
        canvasGroup.alpha = isSelected ? 1f : 0f;
    }
}
