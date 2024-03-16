using Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BasicEnemy : MonoBehaviour, IEnemy
{
    private GameObject target;
    private Rigidbody2D rb;

    public float health;
    public float damage;
    public float range;
    public float speed;

    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage()
    {
        // Will take damage based on player's attack
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        // Check if player is in range and calls Attack()
        float distance = Vector3.Distance(target.transform.position, transform.position);
        
        if (distance <= range)
        {
            Attack();
        }
        else
        {
            Debug.Log("Player is not in range");
        }
    }

    public void FixedUpdate()
    {
        // Tracks and moves toward the player
        // Follow animations
        Vector3 direction = (target.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Rotate the enemy to face the player
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
    }

    public void Attack()
    {
        // Attack animations
        Debug.Log("Attacking");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If enemy collides with ball/orb/chargeblast, then TakeDamage()
        if (other.gameObject.name == "Ball") 
        {
            TakeDamage();
        }
    }
}