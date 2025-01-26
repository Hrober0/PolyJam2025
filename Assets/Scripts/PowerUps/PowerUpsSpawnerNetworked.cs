using HCore;
using Unity.Netcode;
using UnityEngine;

public class PowerUpsSpawnerNetworked : NetworkBehaviour
{

    [SerializeField] private float timeToSpawn = 5f;
    [SerializeField] private int maxActivePowerUps = 2;
    [SerializeField] private PowerUpObject[] powerUps;

    [SerializeField] private MinMax<Vector2> spawnRange;

    private int activePowerUps = 0;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            SpawnCorutine();
        }
    }

    private async Awaitable SpawnCorutine()
    {
        while (true)
        {
            await Awaitable.WaitForSecondsAsync(timeToSpawn);
            var index = Random.Range(0, powerUps.Length);
            var powerUp = Instantiate(powerUps[index]);
            powerUp.GetComponent<NetworkObject>().Spawn();
            var spawnPosition = spawnRange.Random().To3D(0);
            powerUp.transform.position = spawnPosition;
            activePowerUps++;
            powerUp.OnCollected += OnCollect;

            while (activePowerUps >= maxActivePowerUps)
            {
                await Awaitable.FixedUpdateAsync();
            }
        }
    }

    private void OnCollect(PowerUpObject obj)
    {
        obj.OnCollected -= OnCollect;
        activePowerUps--;
    }
}
