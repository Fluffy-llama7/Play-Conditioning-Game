using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


/// <summary>
/// Projectile should shoot at the orb. If it hits the orb, it transfers half of its velocity to the orb.
/// </summary>
public class PlayerProjectile : MonoBehaviour
{
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        // Destroy the bullet after 5 seconds if it doesn't hit the ball
        if (timer >= 10.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if the bullet hits the ball, bullet is destroyed and ball's velocity is half of the bullet's velocity
        Destroy(this.gameObject);
    }
}
