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
    private Animator animator;
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
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance >= range)
        {
            Vector3 direction = (target.transform.position - transform.position);
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ball")
        {
            TakeDamage();
        }
    }
}