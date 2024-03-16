using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody2D ballRigidBody;
    private Rigidbody2D rb;
    private float timer;
    private float force;
    
    void Awake()
    {
        ball = GameObject.Find("Ball");
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        timer = 0.0f;
        force = 20.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Destroy the bullet after 2 seconds if it doesn't hit the ball
        if (timer >= 2.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        // Calculate the direction of the bullet and set its velocity
        Vector3 direction = (ball.transform.position - this.transform.position).normalized;
        rb.velocity = direction * force;

        // Rotate the bullet to face the ball
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the bullet hits the ball, bullet is destroyed and ball's velocity is half of the bullet's velocity
        if (other.gameObject.name == "Ball")
        {
            if (ballRigidBody != null)
            {
                ballRigidBody.velocity = rb.velocity / 2;
            }

            Destroy(this.gameObject);
        }
    }
}
