using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class RangedEnemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    private GameObject prefab;
    private GameObject target;
    private Rigidbody2D rb;

    public float health;
    public float speed;
    public float rotateSpeed;
    public float damage;
    public float range;

    private float fireTime;
    public float fireRate;

    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage()
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        // If in range, attack
        if (Distance() <= range)
        {
            Attack();
        }
    }

    public void FixedUpdate()
    {
        // If not in range, move towards the player
        if (Distance() > range)
        {
            // Tracks and moves toward the player
            // Follow animations
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // Rotate the enemy to face the player
            float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
        }
        else
        {
            // If in range, stop moving
            rb.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        // Shoots at player at fireRate
        if (fireTime <= 0.0f)
        {
            // Shoot animations
            Debug.Log("Shooting");
            Instantiate(prefab, transform.position, Quaternion.identity);
            fireTime = fireRate;
        }
        else
        {
            fireTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If enemy collides with ball/orb/chargeblast, then TakeDamage()
        if (other.gameObject.name == "Ball")
        {
            TakeDamage();
        }
    }

    private float Distance()
    {
        return Vector2.Distance(target.transform.position, transform.position);
    }
}
