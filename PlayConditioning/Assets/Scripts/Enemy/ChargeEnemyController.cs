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
    public float range = 3f;
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
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        animator.SetFloat("AnimMoveX", direction.x);
        animator.SetFloat("AnimMoveY", direction.y);

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
        
    }

    public void Attack()
    {
        if (canCharge)
        {
            animator.SetBool("AnimAttack", true);
            var targetPosition = target.transform.position;
            var direction = targetPosition - transform.position;
            StartCoroutine(Charge(direction));
            canCharge = false;
            StartCoroutine(ChargeDelay());
        }
    }

    private IEnumerator Charge(Vector3 direction)
    {
        rb.velocity = direction * speed;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
    }

    private IEnumerator ChargeDelay()
    {
        yield return new WaitForSeconds(chargeTime);
        canCharge = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Ball")
        {
            TakeDamage();
        }
    }
}