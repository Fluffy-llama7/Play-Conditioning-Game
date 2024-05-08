using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class TankEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField]
    private GameObject prefab;
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;
    public Animator animator;
    public float health = 10f;
    public float speed = 5f;
    public float range = 5f;
    public float fireTime = 2f;
    private bool canFire = true;

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
            rb.velocity = Vector2.zero;
            Attack();
        }

    }

    public void TakeDamage()
    {
        
    }

    public void Attack()
    {
        animator.SetBool("AnimAttack", true);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ball")
        {
            TakeDamage();
        }
    }
}
