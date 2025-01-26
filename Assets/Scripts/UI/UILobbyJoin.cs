using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyJoin : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;

    private void Start()
    {
        startHostButton.onClick.AddListener(() =>
        {
            GameNM.Instance.StartHost();
            Loader.LoadScene(Loader.Scene.RumbaSelect);
        });
        startClientButton.onClick.AddListener(() =>
        {
            GameNM.Instance.StartClient();
        });
    }
}
