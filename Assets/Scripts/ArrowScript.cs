using System.Collections;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float destroyDelay = 0.1f; // The delay before the arrow is destroyed after hitting the ground

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Trap"))
        {
            
            StartCoroutine(DelayedDestroy(gameObject, destroyDelay));
        }
    }

    IEnumerator DelayedDestroy(GameObject objectToDestroy, float delay)
    {
        
        yield return new WaitForSeconds(delay);

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
}