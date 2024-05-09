using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class BasicEnemyController : MonoBehaviour, IEnemy
{
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;
    public Animator animator;
    public float health = 10f;
    public float speed = 5f;
    public float range = 5f;

    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        direction = (target.transform.position - transform.position).normalized;

        animator.SetFloat("AnimMoveX", direction.x);
        animator.SetFloat("AnimMoveY", direction.y);
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance >= range)
        {
            animator.SetBool("AnimAttack", false);
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
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        animator.SetBool("AnimAttack", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ball")
        {
            TakeDamage();
        }
    }
}