using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 5.0f;
    private GameObject ball;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = GameObject.Find("Ball");

        // Shoots the bullet towards the ball
        Vector3 direction = ball.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Rotates the bullet towards the ball
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the bullet collides with the ball
        if (other.gameObject.name == "Ball")
        {
            Rigidbody2D ballRigidBody = ball.GetComponent<Rigidbody2D>();

            if (ballRigidBody != null)
            {
                ballRigidBody.velocity = rb.velocity;
            }

            // Destroys the bullet
            Destroy(this.gameObject);
        }
    }
}
