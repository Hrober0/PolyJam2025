using System;
using UnityEngine;

public class PowerUpObject : MonoBehaviour
{
    public event Action<PowerUpObject> OnCollected;

    [SerializeField] private PowerUpConfigSO config;

    public void PickUp(Player player)
    {
        config.Apply(player.gameObject);
        OnCollected?.Invoke(this);

        Destroy(gameObject);
    }
}

