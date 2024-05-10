using Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BasicEnemyController : MonoBehaviour, IEnemy
{
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;

    public Animator animator;
    public float health = 10f;
    public float speed = 5f;
    public float range = 2f;

    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        direction = (target.transform.position - transform.position).normalized;

        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance >= range)
        {
            animator.SetBool("Basic", false);
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            Attack();
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        Debug.Log("Charge's Health: " + health);

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Attack()
    {
        animator.SetBool("Basic", true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Orb")
        {
            TakeDamage();
        }
    }
}