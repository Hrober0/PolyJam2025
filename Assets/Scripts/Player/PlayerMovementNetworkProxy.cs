using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementNetworkProxy : NetworkBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ActiveCall activeCall;

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            MoveLeftServerRpc(context.phase);
        }
    }
    public void MoveRight(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            MoveRightServerRpc(context.phase);
        }
    }
    public void MoveBack(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            MoveBackServerRpc(context.phase);
        }
    }
    public void CallActive(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            CallActiveServerRpc(context.phase);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void MoveLeftServerRpc(InputActionPhase action)
    {
        playerMovement.MoveLeft(action);
    }

    [ServerRpc(RequireOwnership = false)]
    private void MoveRightServerRpc(InputActionPhase action)
    {
        playerMovement.MoveRight(action);
    }

    [ServerRpc(RequireOwnership = false)]
    private void MoveBackServerRpc(InputActionPhase action)
    {
        playerMovement.MoveBack(action);
    }

    [ServerRpc(RequireOwnership = false)]
    private void CallActiveServerRpc(InputActionPhase action)
    {
        activeCall.CallActive(action);
    }

}
