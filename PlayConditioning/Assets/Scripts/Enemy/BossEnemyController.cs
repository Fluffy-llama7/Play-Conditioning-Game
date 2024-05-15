using Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BossEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float health = 10f;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float range = 2f;
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;

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
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }

    public void Attack()
    {

    }
}