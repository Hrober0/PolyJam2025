using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerLocal : MonoBehaviour
{
    public UnityEvent<Player> OnPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            OnPlayerEnter?.Invoke(player);
        }
    }
}
