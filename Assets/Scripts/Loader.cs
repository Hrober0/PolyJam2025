using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Lobby,
        RumbaSelect,
        NetworkLevel,
    }

    public static void LoadScene(Scene scene)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }
}
