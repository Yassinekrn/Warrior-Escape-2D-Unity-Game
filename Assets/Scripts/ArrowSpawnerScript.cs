using UnityEngine;
using System.Collections;

public class ArrowSpawnerScript : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float spawnInterval = 1f; // spawn every 1 second by default
    public float spawnRadius = 5f; // spawn within 5 units of the spawn point by default
    public float minVelocity = 5f; // minimum horizontal velocity of the arrow
    public float maxVelocity = 10f; // maximum horizontal velocity of the arrow
    private float x = 26.5f;
    private float y1 = 6f;
    private float y2 = 12f;

    void Start()
    {
        StartCoroutine(SpawnArrows());
    }

    IEnumerator RotateArrow(GameObject arrow)
    {
        if (arrow == null)
        {
            yield break; // Exit the Coroutine if the arrow object is null
        }

        float rotationDuration = 1.5f; // The time it takes to rotate the arrow (in seconds)
        Quaternion startRotation = arrow.transform.rotation; // The arrow's initial rotation
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -90f); // The target rotation for the arrow (vertical)
        float t = 0f; // The current time (used to interpolate between startRotation and targetRotation)
        while (t < rotationDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / rotationDuration; // A value between 0 and 1 representing how far along the rotation we are
            if (arrow == null)
            {
                yield break; // Exit the Coroutine if the arrow object is null
            }
            arrow.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, normalizedTime); // Interpolate the rotation between startRotation and targetRotation based on the current time
            yield return null;
        }
        if (arrow == null)
        {
            yield break; // Exit the Coroutine if the arrow object is null
        }
        arrow.transform.rotation = targetRotation; // Ensure the arrow is set to its target rotation once the Coroutine has finished
    }


    IEnumerator SpawnArrows()
    {
        while (true)
        {
            float y = Random.Range(y1, y2);
            Vector3 position = new Vector3(x + Random.Range(-spawnRadius, spawnRadius), y, 0f);
            GameObject arrow = Instantiate(arrowPrefab, position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minVelocity, maxVelocity), 0f);
            StartCoroutine(RotateArrow(arrow));
            arrow.GetComponent<Collider2D>().isTrigger = false; // Set the Collider2D to non-trigger so it detects collisions
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
