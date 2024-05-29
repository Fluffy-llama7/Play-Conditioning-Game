using Enemy;
using System.Collections;
using UnityEngine;

public class BossEnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float basicRange = 0f; // Separate range for Basic attack
    [SerializeField] private float tankRange = 10f; // Separate range for Tank attack
    [SerializeField] private float chargeRange = 10f; // Separate range for Charge attack
    [SerializeField] private float chargeTime = 2f;
    [SerializeField] private float fireTime = 1f;
    [SerializeField] private float damage = 4f;

    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 direction;

    private bool canCharge = true;
    private bool canFire = true; 
    private bool isCharging = false; 

    private enum AttackMode { Basic, Tank, Charge }
    private AttackMode currentAttackMode;

    private void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameManager.instance.AddEnemy(this);
        StartCoroutine(SwitchAttackModeCycle());
    }

    private void OnDestroy()
    {
        GameManager.instance.RemoveEnemy(this);
    }

    public void Update()
    {
        if (target == null) return;

        direction = (target.transform.position - transform.position).normalized;

        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    private void FixedUpdate()
    {
        if (target == null || isCharging) return;

        float currentRange = GetCurrentRange();
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance >= currentRange)
        {
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
            Attack();
        }
    }

    private float GetCurrentRange()
    {
        switch (currentAttackMode)
        {
            case AttackMode.Basic:
                return basicRange;
            case AttackMode.Tank:
                return tankRange;
            case AttackMode.Charge:
                return chargeRange;
            default:
                return 0f;
        }
    }

    public void Attack()
    {
        switch (currentAttackMode)
        {
            case AttackMode.Basic:
                BasicAttack();
                break;
            case AttackMode.Tank:
                TankAttack();
                break;
            case AttackMode.Charge:
                ChargeAttack();
                break;
        }
    }

    private IEnumerator SwitchAttackModeCycle()
    {
        while (true)
        {
            currentAttackMode = AttackMode.Basic;
            Debug.Log("Switching to Basic Attack");
            yield return new WaitForSeconds(4f); // Basic attack for 4 seconds

            currentAttackMode = AttackMode.Tank;
            Debug.Log("Switching to Tank Attack");
            yield return new WaitForSeconds(4f); // Tank attack for 4 seconds

            currentAttackMode = AttackMode.Charge;
            Debug.Log("Switching to Charge Attack");
            yield return new WaitForSeconds(1f); // Charge attack for 1 second
        }
    }

    private void BasicAttack()
    {
        Debug.Log("Basic Attack");
        ResetAnimations();
        animator.SetBool("Basic", true);
    }

    private void TankAttack()
    {
        Debug.Log("Tank Attack");
        ResetAnimations();
        animator.SetBool("Tank", true);

        if (canFire)
        {
            InstantiateProjectilesInAllDirections();
            canFire = false;
            StartCoroutine(ShootDelay());
        }
    }

    private void InstantiateProjectilesInAllDirections()
    {
        for (float angle = 0; angle < 360; angle += 45)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(projectilePrefab, transform.position, rotation);
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireTime);
        canFire = true;
    }

    private void ChargeAttack()
    {
        Debug.Log("Charge Attack");
        ResetAnimations();
        animator.SetBool("Charge", true);

        if (canCharge)
        {
            var targetPosition = target.transform.position;
            var chargeDirection = (targetPosition - transform.position).normalized;
            StartCoroutine(Charge(chargeDirection));
            canCharge = false;
            StartCoroutine(ChargeDelay());
        }
    }

    private IEnumerator Charge(Vector3 chargeDirection)
    {
        Debug.Log("Starting Charge");
        isCharging = true;
        Debug.Log("isCharging: " + isCharging);
        rb.velocity = chargeDirection * speed * 4; 
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector2.zero;
        Debug.Log("Charge Complete");
        isCharging = false;
        animator.SetBool("Charge", false);
    }

    private IEnumerator ChargeDelay()
    {
        yield return new WaitForSeconds(chargeTime);
        canCharge = true;
        Debug.Log("Can charge again");
    }

    private void ResetAnimations()
    {
        animator.SetBool("Basic", false);
        animator.SetBool("Tank", false);
        animator.SetBool("Charge", false);
    }

    public float Damage => damage;
}
