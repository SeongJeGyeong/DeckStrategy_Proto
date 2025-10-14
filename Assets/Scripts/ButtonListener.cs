using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    public Image targetImage;
    public void OnButtonClick()
    {
        if (targetImage != null)
        {
            if(targetImage.enabled == false)
            {
                targetImage.enabled = true; // Ȱ��ȭ
            }
            else
            {
                targetImage.enabled = false;
            }
        }
    }
}
