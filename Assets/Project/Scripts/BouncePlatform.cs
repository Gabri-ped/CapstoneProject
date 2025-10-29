using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null && collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // azzera la y
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}
