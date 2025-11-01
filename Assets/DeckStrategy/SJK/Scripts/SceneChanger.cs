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
        SceneManager.LoadScene("BattleScene_sjk");
    }

    public void ChangeToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ChangeToResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
