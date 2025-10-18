using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    public Button button;
    [SerializeField]
    private CanvasGroup canvasGroup;

    public bool isSelected = false;
    private float targetAlpha;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void ButtonClicked()
    {
        isSelected = !isSelected;
        canvasGroup.alpha = isSelected ? 1f : 0f;
    }
}
