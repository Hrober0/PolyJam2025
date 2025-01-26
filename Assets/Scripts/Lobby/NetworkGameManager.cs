using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkGameManager : NetworkBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private List<Transform> spawnPoints;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            NetworkManager.SceneManager.OnLoadEventCompleted += SpawnPlayers;
        }
    }

    private void SpawnPlayers(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        int index = 0;
        foreach (var clientId in NetworkManager.ConnectedClientsIds)
        {
            var spawnPoint = spawnPoints[index];
            index++;

            Player player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            player.name = $"Player{index}";

            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
    }
}
