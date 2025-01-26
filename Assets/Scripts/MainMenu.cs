using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StatMulti()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void StartLocal()
    {
        SceneManager.LoadScene("RumbaSelect");
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}
