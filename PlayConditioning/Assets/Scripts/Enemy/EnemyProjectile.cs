using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Enemy;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject target;
    private float timer = 2f;
    private float force = 20f;

    void Awake()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Destroy the bullet after 2 seconds if it doesn't hit the ball
        if (timer >= 10.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        // Calculate the direction of the bullet and set its velocity
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        rb.velocity = direction * force;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
