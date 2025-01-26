using Unity.Netcode;
using UnityEngine;

public class BubbleInteractionChandlerNetworked : NetworkBehaviour
{
    [SerializeField] private Bubble bubble;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer)
            return;

        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            SetPlayerClientRpc(player.Data.clientId);
        }
    }

    [ClientRpc]
    private void SetPlayerClientRpc(ulong clientId)
    {
        bubble.SetPlayer(GameNM.Instance.GetPlayerDataById(clientId));
    }
}
