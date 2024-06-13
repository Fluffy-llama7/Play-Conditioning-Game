using System.Collections;
using UnityEngine;
using Enemy;

public class TankEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float range = 10f;
    [SerializeField] private float fireTime = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float projectileForce = 10f;
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool canFire = true;

    private void Start()
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

    private void FixedUpdate()
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
            InstantiateProjectile();

            canFire = false;
            StartCoroutine(ShootDelay());
        }
    }


    private void InstantiateProjectile()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectileObject.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
    }


    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireTime);
        canFire = true;
    }

    public float Damage => damage;
}
