using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultCharacterDisplay : MonoBehaviour
{
    private TeamMVPData mvpData;

    void Start()
    {
        mvpData = Resources.Load<TeamMVPData>("TeamMVPData");
        if (mvpData == null)
        {
            Debug.LogError("TeamMVPData.asset �� ã�� �� �����ϴ�! (Assets/Resources/TeamMVPData.asset)");
            return;
        }

        // �� ���� �ڵ� ����
        SetSlot("MVP1", mvpData.char1Name, mvpData.char1Color);
        SetSlot("MVP2", mvpData.char2Name, mvpData.char2Color);
        SetSlot("MVP3", mvpData.char3Name, mvpData.char3Color);
        SetSlot("MVP4", mvpData.char4Name, mvpData.char4Color);
        SetSlot("MVP5", mvpData.char5Name, mvpData.char5Color);
    }

    private void SetSlot(string slotName, string charName, Color color)
    {
        Transform slot = transform.Find($"CharacterList_test/{slotName}");
        if (slot == null) return;

        Image image = slot.Find("Image1")?.GetComponent<Image>();
        TMP_Text text = slot.Find("Text_Name1")?.GetComponent<TMP_Text>();

        if (image != null)
        {
            image.color = color; // ĳ���� ���� ǥ��
        }

        if (text != null)
        {
            text.text = string.IsNullOrEmpty(charName) ? "-" : charName;
        }
    }
}