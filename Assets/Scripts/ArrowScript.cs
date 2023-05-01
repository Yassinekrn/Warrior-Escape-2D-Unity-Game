using System.Collections;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float destroyDelay = 0.5f; // The delay before the arrow is destroyed after hitting the ground

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Arrow collided with ground.");
            StartCoroutine(DelayedDestroy(gameObject, destroyDelay));
        }
    }

    IEnumerator DelayedDestroy(GameObject objectToDestroy, float delay)
    {
        Debug.Log("DelayedDestroy coroutine called.");
        yield return new WaitForSeconds(delay);

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
}
