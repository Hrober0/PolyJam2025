using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playOnlineButton;
    [SerializeField] private Button playLocalButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        playOnlineButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Loader.Scene.Lobby.ToString());
        });
        playLocalButton.onClick.AddListener(() => {
            SceneManager.LoadScene(Loader.Scene.LocalLevel.ToString());
        });
        exitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
