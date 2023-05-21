using UnityEngine;

public class arrSpawnerTL : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float spawnInterval = 1f; // Spawn every 1 second by default
    public float x1 = 25.5f; // Minimum x position
    public float x2 = 30f; // Maximum x position
    public float y = 13.5f; // Fixed y position

    void Start()
    {
        InvokeRepeating("SpawnArrow", 0f, spawnInterval);
    }

    void SpawnArrow()
    {
        float randomX = Random.Range(x1, x2);
        Vector3 spawnPosition = new Vector3(randomX, y, 0f);
        Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);
    }
}
