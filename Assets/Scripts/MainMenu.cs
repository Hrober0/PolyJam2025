using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StatMulti()
    {
        SceneManager.LoadScene("MultiplayerGame");
    }

    public void StartLocal()
    {
        SceneManager.LoadScene("CleaningTest");
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}
