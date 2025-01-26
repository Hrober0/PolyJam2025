using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyJoin : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;
    [SerializeField] private TMP_InputField ipField;
    [SerializeField] private TMP_InputField portField;

    private UnityTransport transport;

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

        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        ipField.text = transport.ConnectionData.Address;
        ipField.onSubmit.AddListener((adres) =>
        {
            transport.SetConnectionData(adres, transport.ConnectionData.Port);
        });
        portField.text = transport.ConnectionData.Port.ToString();
        portField.onSubmit.AddListener((portStr) =>
        {
            if (ushort.TryParse(portStr, out ushort port))
            {
                transport.SetConnectionData(transport.ConnectionData.Address, port);
            }
            else
            {
                Debug.LogWarning($"Incorrect port! {portStr}");
                portField.text = transport.ConnectionData.Port.ToString();
            }
        });
    }
}
