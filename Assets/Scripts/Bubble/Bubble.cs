using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public event Action OnPlayerChanged;

    public PlayerData? PlayerData { get; private set; }

    public void SetPlayer(PlayerData data)
    {
        PlayerData = data;
        OnPlayerChanged?.Invoke();
    }
}
