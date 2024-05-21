using Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BasicEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float range = 2f;
    [SerializeField]
    private float damage = 1f;
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameManager.instance.AddEnemy(this);
    }

    void OnDestroy()
    {
        GameManager.instance.RemoveEnemy(this);
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
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
    }
    
    public float Damage { get { return damage; } }
}