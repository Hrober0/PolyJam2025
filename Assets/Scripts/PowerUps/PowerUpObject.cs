using DG.Tweening;
using System;
using UnityEngine;

public class PowerUpObject : MonoBehaviour
{
    public event Action<PowerUpObject> OnCollected;

    [SerializeField] private PowerUpConfigSO config;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f).SetEase(Ease.OutQuad);
    }

    public void PickUp(Player player)
    {
        config.Apply(player.gameObject);
        OnCollected?.Invoke(this);

        transform.DOScale(0, 0.3f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            transform.DOKill();
            Destroy(gameObject);
        });
    }
}

