using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    // UI ��ư�� OnClick �̺�Ʈ�� ����
    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}