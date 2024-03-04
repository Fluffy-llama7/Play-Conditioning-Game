using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody2D rb;
    private float timer = 0.0f;
    private float force = 20.0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = GameObject.Find("Ball");

        Vector3 direction = ball.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ball")
        {
            Rigidbody2D ballRigidBody = ball.GetComponent<Rigidbody2D>();

            if (ballRigidBody != null)
            {
                ballRigidBody.velocity = rb.velocity / 2;
            }

            Destroy(this.gameObject);
        }
    }
}
