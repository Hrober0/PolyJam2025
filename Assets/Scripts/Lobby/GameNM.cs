using System;
using Unity.Netcode;
using UnityEngine;

public class GameNM : NetworkBehaviour
{
    public event Action OnPlayerDataListChanged;

    public static GameNM Instance { get; private set; }

    public NetworkList<PlayerData> PlayerDataList { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);

        PlayerDataList = new NetworkList<PlayerData>();
        PlayerDataList.OnListChanged += NotifyOnPlayerDataListChanged;
    }

    public void StartHost()
    {
        Debug.Log("StartHost");
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedBack;
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        Debug.Log("StartClient");
        NetworkManager.Singleton.StartClient();
    }

    public bool IsPlayerConnected(int playerIndex)
    {
        return playerIndex < PlayerDataList.Count;
    }

    private void OnClientConnectedBack(ulong clientId)
    {
        PlayerDataList.Add(new PlayerData
        {
            clientId = clientId,
        });
        Debug.Log($"Joined {clientId}");
    }

    private void NotifyOnPlayerDataListChanged(NetworkListEvent<PlayerData> _)
    {
        OnPlayerDataListChanged?.Invoke();
    }

}
