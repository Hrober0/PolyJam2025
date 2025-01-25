using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn = 5f;

    public Transform[] powerUps;

    [SerializeField]
    Vector2 spawnRangeX;
    [SerializeField]
    Vector2 spawnRangeY;

    private void Start()
    {
        SpawnCorutine();
    }

    private async Awaitable SpawnCorutine()
    {
        while (true)
        {
            await Awaitable.WaitForSecondsAsync(timeToSpawn);
            Transform powerUp = powerUps[Random.Range(0, powerUps.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), 0, Random.Range(spawnRangeY.x, spawnRangeY.y));
            Debug.Log("Spawning power up at " + spawnPosition);
            Instantiate(powerUp, spawnPosition, Quaternion.identity);
        }
    }
}
