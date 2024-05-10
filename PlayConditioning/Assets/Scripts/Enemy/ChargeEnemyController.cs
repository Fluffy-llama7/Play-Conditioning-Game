using Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ChargeEnemyController : MonoBehaviour, IEnemy
{
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;

    public Animator animator;
    public float health = 10f;
    public float speed = 5f;
    public float range = 2f;
    public float chargeTime = 2f;
    private bool canCharge = true;

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
            animator.SetBool("Charge", false);
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            Attack();
            animator.SetBool("Charge", true);
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
        if (canCharge)
        {
            var targetPosition = target.transform.position;
            var direction = targetPosition - transform.position;
            StartCoroutine(Charge(direction));
            canCharge = false;
            StartCoroutine(ChargeDelay());
        }
    }

    private IEnumerator Charge(Vector3 direction)
    {
        rb.velocity = direction * speed * 2;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Charge", false);
        rb.velocity = Vector2.zero;
    }

    private IEnumerator ChargeDelay()
    {
        yield return new WaitForSeconds(chargeTime);
        canCharge = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Orb")
        {
            TakeDamage();
        }
    }
}