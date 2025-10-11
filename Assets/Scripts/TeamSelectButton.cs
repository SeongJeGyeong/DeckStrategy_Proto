using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TeamSelectButton : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Toggle toggle;

    void Awake()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        toggle.isOn = isOn;
        toggle.image.color = isOn ? Color.gray : Color.white;
        Debug.Log("toggled");
    }
}
