using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveCall : MonoBehaviour
{
    public void MoveLeft(InputAction.CallbackContext context) => CallActive(context.phase);
    public void CallActive(InputActionPhase action)
    {
        OnActiveCall?.Invoke();
    }

    public delegate void ActiveCallDelegate();
    public event ActiveCallDelegate OnActiveCall;
}
