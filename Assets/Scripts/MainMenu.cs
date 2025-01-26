using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StatMulti()
    {
        SceneManager.LoadScene(Loader.Scene.Lobby.ToString());
    }

    public void StartLocal()
    {
        SceneManager.LoadScene(Loader.Scene.LocalLevel.ToString());
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}
