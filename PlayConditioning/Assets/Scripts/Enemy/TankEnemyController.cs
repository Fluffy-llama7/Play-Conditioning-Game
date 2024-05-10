using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class TankEnemyController : MonoBehaviour, IEnemy
{
    public GameObject prefab;
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool canFire = true;

    public Animator animator;
    public float health = 5f;
    public float speed = 5f;
    public float range = 5f;
    public float fireTime = 2f;

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
            animator.SetBool("Attack", false);
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
            Attack();
        }
    }

    public void TakeDamage()
    {
        health -= 1;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);

        if (canFire)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            canFire = false;
            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireTime);
        canFire = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Orb")
        {
            TakeDamage();
        }
    }
}
