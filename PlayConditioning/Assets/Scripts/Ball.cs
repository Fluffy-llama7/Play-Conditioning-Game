using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Prevents ball from moving outside of the field
        if (other.gameObject.name == "Wall")
        {
            // Reflects the ball's velocity when it collides with the wall
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }
}
