using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    // UI ��ư�� OnClick �̺�Ʈ�� ����
    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("ResultScene");
    }
}