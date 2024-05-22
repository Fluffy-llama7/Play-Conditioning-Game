using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float damage = 5.0f;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;

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
        if (other.gameObject.name == "Walls" || other.gameObject.tag == "Player Projectile")
        {
            // Reflects the ball's velocity when it collides with the wall
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }

    public float Damage
    {
        get { return damage; }
    }
}
