using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Enemy;

public class BossEnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float damage = 2.0f;
    private float force = 20f;
    private float timer = 0.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Destroy the bullet after 5 seconds if it doesn't hit the ball
        if (timer >= 2.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        // Calculate the direction of the bullet and set its velocity
        rb.velocity = transform.right * force;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }

    public float Damage { get { return damage; } }
}
