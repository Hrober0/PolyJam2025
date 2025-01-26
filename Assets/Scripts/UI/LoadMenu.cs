using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] private float duration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadMenuAsync();
    }

    private async Awaitable LoadMenuAsync()
    {
        await Awaitable.WaitForSecondsAsync(duration);
        SceneManager.LoadScene(Loader.Scene.MainMenu.ToString());

    }
}
