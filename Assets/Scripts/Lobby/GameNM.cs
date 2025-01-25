using System;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public PlayerData GetPlayerDataByIndex(int playerIndex)
    {
        return PlayerDataList[playerIndex];
    }
    public PlayerData GetPlayerDataById(ulong clientId)
    {
        foreach (PlayerData playerData in PlayerDataList)
        {
            if (playerData.clientId == clientId)
            {
                return playerData;
            }
        }
        Debug.LogError($"Player data with id {clientId} not found");
        return default;
    }
    public PlayerData GetCurrentPlayerData()
    {
        var localId = NetworkManager.Singleton.LocalClientId;
        foreach (PlayerData playerData in PlayerDataList)
        {
            if (playerData.clientId == localId)
            {
                return playerData;
            }
        }
        Debug.LogError("Current data not found");
        return default;
    }
    public bool IsCurrentPlayerDataSet()
    {
        var localId = NetworkManager.Singleton.LocalClientId;
        foreach (PlayerData playerData in PlayerDataList)
        {
            if (playerData.clientId == localId)
            {
                return true;
            }
        }
        return false;
    }

    private void OnClientConnectedBack(ulong clientId)
    {
        PlayerDataList.Add(new PlayerData
        {
            clientId = clientId,
            color = GetRandomColor(),
        });
        Debug.Log($"Joined {clientId}");
    }

    private void NotifyOnPlayerDataListChanged(NetworkListEvent<PlayerData> _)
    {
        OnPlayerDataListChanged?.Invoke();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerColorServerRpc(Color color, ServerRpcParams serverRpcParams = default)
    {
        SetPlayerColor(serverRpcParams.Receive.SenderClientId, color);
    }
    private void SetPlayerColor(ulong clientId, Color color)
    {
        for (int i = 0; i < PlayerDataList.Count; i++)
        {
            if (PlayerDataList[i].clientId == clientId)
            {
                var data = PlayerDataList[i];
                data.color = color;
                PlayerDataList[i] = data;
            }
        }
    }
    private Color GetRandomColor()
    {
        byte red = (byte)Random.Range(0, 255);
        byte green = (byte)Random.Range(0, 255);
        byte blue = (byte)Random.Range(0, 255);
        byte alpha = 255;

        return new Color32(red, green, blue, alpha);
    }
}
