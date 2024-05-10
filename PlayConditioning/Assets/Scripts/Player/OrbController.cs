using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    public Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Prevents ball from moving outside of the field
        if (other.gameObject.name == "Walls")
        {
            // Reflects the ball's velocity when it collides with the wall
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }
}
