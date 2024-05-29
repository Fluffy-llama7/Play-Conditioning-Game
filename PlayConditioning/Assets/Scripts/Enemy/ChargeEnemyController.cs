using Enemy;
using System.Collections;
using UnityEngine;

public class ChargeEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float range = 2f;
    [SerializeField] private float chargeTime = 2f;
    [SerializeField] private float damage = 2f;

    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool canCharge = true;

    private void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameManager.instance.AddEnemy(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.RemoveEnemy(this);
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
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);

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
        rb.velocity = direction.normalized * speed * 2;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Attack", false);
        rb.velocity = Vector2.zero;
    }

    private IEnumerator ChargeDelay()
    {
        yield return new WaitForSeconds(chargeTime);
        canCharge = true;
    }

    public float Damage => damage;
}
