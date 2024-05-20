using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class TankEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float range = 8f;
    [SerializeField]
    private float fireTime = 1f;
    [SerializeField]
    private float damage = 1f;
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool canFire = true;

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
            rb.velocity = Vector2.zero;
            Attack();
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

    public float Damage { get { return damage; } }
}
