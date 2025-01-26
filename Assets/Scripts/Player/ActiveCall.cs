using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveCall : MonoBehaviour
{
    public void CallActive(InputAction.CallbackContext context)
    {
        OnActiveCall?.Invoke();
    }

    public delegate void ActiveCallDelegate();
    public event ActiveCallDelegate OnActiveCall;
}
