using Unity.Netcode;
using UnityEngine;

public class PlayerToolSynchronizer : NetworkBehaviour
{
    [SerializeField] private PlayerToolsController controller;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            controller.OnToolChanged += UpdateClientRpc;
        }
    }
    public override void OnNetworkDespawn()
    {
        controller.OnToolChanged -= UpdateClientRpc;
    }

    [ClientRpc]
    private void UpdateClientRpc(PlayerToolsController.Tool tool, bool state)
    {
        if (!IsServer)
        {
            controller.SetTool(tool, state);
        }
    }
}
