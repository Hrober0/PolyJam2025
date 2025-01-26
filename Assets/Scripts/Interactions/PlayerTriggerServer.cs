using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerServer : NetworkBehaviour
{
    public UnityEvent<Player> OnPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer)
        {
            return;
        }
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            OnPlayerEnter?.Invoke(player);
        }
    }
}
