using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    [SerializeField] private PowerUpConfigSO config;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            config.Apply(player.gameObject);
            Destroy(gameObject);
        }
    }
}

