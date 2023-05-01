using UnityEngine;
using System.Collections;

public class healthBoostSpawnerScript : MonoBehaviour
{
    public GameObject healthBoostPrefab;
    public float spawnInterval = 60f; // spawn every 60 seconds by default
    public float spawnRadius = 10f; // spawn within 10 units of the arena center by default
    private float x1 = 26f;
    private float x2 = 56f;

    void Start()
    {
        StartCoroutine(SpawnHealthBoosts());
    }

    IEnumerator SpawnHealthBoosts()
    {
        while (true)
        {
            float x = Random.Range(x1 + spawnRadius, x2 - spawnRadius);
            Vector3 position = new Vector3(x, 0f, 0f);
            Instantiate(healthBoostPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
