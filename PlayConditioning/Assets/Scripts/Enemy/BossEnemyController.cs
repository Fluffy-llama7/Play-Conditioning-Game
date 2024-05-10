using Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BossEnemyController : MonoBehaviour, IEnemy
{

    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;
    public GameObject prefab;
    public Animator animator;
    public float health = 10f;
    public float speed = 5f;
    public float range = 2f;

    private int attackType = 0;
    private float attackDelay = 2f;

    private ChargeEnemyController chargeEnemyController;
    private TankEnemyController tankEnemyController;
    private BasicEnemyController basicEnemyController;

    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        chargeEnemyController = gameObject.AddComponent<ChargeEnemyController>();
        tankEnemyController = gameObject.AddComponent<TankEnemyController>();
        basicEnemyController = gameObject.AddComponent<BasicEnemyController>();


        animator.SetBool("Basic", false);
        animator.SetBool("Charge", false);
        animator.SetBool("Tank", false);
    }

    public void Update()
    {
        direction = (target.transform.position - transform.position).normalized;

        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);

        if (attackDelay <= 0)
        {
            Attack();
            attackDelay = 3f;
        }
        else
        {
            attackDelay -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance >= range)
        {
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        Debug.Log("Boss's Health: " + health);

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Attack()
    {
        attackType = Random.Range(0, 3);

        switch(this.attackType)
        {
            case 0:
                // basicEnemyController.Attack();
                animator.SetBool("Charge", false);
                animator.SetBool("Tank", false);
                break;
            case 1:
                // chargeEnemyController.Attack();
                animator.SetBool("Basic", false);
                animator.SetBool("Tank", false);
                break;
            case 2:
                // tankEnemyController.Attack();
                animator.SetBool("Basic", false);
                animator.SetBool("Charge", false);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Orb")
        {
            TakeDamage();
        }

        if (other.gameObject.name == "Player")
        {
            Debug.Log("Boss Enemy Hit");
        }
    }
}