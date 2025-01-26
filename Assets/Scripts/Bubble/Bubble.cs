using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public event Action OnPlayerChanged;

    public Player Player { get; private set; }

    public void SetPlayer(Player player)
    {
        Player = player;
        OnPlayerChanged?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            SetPlayer(player);
        }
    }
}
