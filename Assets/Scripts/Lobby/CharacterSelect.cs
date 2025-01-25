using System;
using Unity.Netcode;
using UnityEngine;

public class CharacterSelect : NetworkBehaviour
{
    public event Action OnPlayerReadyChange;

    public static CharacterSelect Instance { get; private set; }

    private NetworkList<ulong> readyPlayers;

    private void Awake()
    {
        Instance = this;
        readyPlayers = new();
        readyPlayers.OnListChanged += (_) => OnPlayerReadyChange?.Invoke();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerReadyServerRpc(bool isReady, ServerRpcParams serverRpcParams = default)
    {
        var senderClientId = serverRpcParams.Receive.SenderClientId;
        if (IsPlayerReady(senderClientId) == isReady)
        {
            Debug.LogWarning("Player is already ready");
            return;
        }
        if (isReady)
        {
            readyPlayers.Add(senderClientId);
        }
        else
        {
            readyPlayers.Remove(senderClientId);
        }

        foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!IsPlayerReady(clientId))
            {
                Debug.Log($"Player {clientId} is not ready yet");
                return;
            }
        }

        Debug.Log("All Ready");
    }

    public bool IsPlayerReady(ulong clientId)
    {
        return readyPlayers.Contains(clientId);
    }
}
