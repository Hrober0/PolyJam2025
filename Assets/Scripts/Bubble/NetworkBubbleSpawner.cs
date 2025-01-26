using Unity.Netcode;
using UnityEngine;

public class NetworkBubbleSpawner : NetworkBehaviour
{
    [SerializeField] private Bubble bubblePrefab;

    public static NetworkBubbleSpawner Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (IsServer)
        {
            SpawnBubbleServerRpc(new Vector3(7.5, 7.5, 7.5));
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnBubbleServerRpc(Vector3 pos)
    {
        var instance = Instantiate(bubblePrefab, pos, Quaternion.identity);;
        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();
    }
}
