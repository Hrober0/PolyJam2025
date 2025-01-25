using System;
using Unity.Netcode;
using UnityEngine;

public class PowerUpPickerNetworked : NetworkBehaviour
{
    [SerializeField] private PowerUpConfigSO config;

    public event Action OnCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer)
        {
            return;
        }
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            config.Apply(player.gameObject);
            OnCollected?.Invoke();
            Destroy(gameObject);       
        }
    }
}