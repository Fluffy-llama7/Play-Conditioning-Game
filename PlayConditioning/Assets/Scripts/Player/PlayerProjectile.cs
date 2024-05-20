using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


/// <summary>
/// Projectile should shoot at the orb. If it hits the orb, it transfers half of its velocity to the orb.
/// </summary>
public class PlayerProjectile : MonoBehaviour
{
    private GameObject orb;
    private Rigidbody2D orbRB;
    private Rigidbody2D rb;
    private float force = 20f;
    private float timer = 0.0f;
    
    void Awake()
    {
        orb = GameObject.Find("Orb");
        orbRB = orb.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Destroy the bullet after 5 seconds if it doesn't hit the ball
        if (timer >= 5.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        // Calculate the direction of the bullet and set its velocity
        Vector3 direction = (orb.transform.position - this.transform.position).normalized;
        rb.velocity = direction * force;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if the bullet hits the ball, bullet is destroyed and ball's velocity is half of the bullet's velocity
        if (other.gameObject.name == "Orb")
        {
            if (orbRB != null)
            {
                orbRB.velocity = rb.velocity / 2;
            }

            Destroy(this.gameObject);
        }
    }
}
