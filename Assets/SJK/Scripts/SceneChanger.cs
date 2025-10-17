using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToDeckEdit()
    {
        SceneManager.LoadScene("DeckEditsScene");
    }

    public void ChangeToBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void ChangeToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
