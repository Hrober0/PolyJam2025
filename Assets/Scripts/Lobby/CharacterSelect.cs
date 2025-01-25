using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSelect : NetworkBehaviour
{
    private readonly Dictionary<ulong, bool> readyStatus = new();

    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerReadyServerRpc(bool isReady, ServerRpcParams serverRpcParams = default)
    {
        readyStatus[serverRpcParams.Receive.SenderClientId] = isReady;

        foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!readyStatus.TryGetValue(clientId, out var ready) || !ready)
            {
                return;
            }
        }

        Debug.Log("Ready");
    }
}
