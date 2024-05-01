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
    private Animator animator;
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
