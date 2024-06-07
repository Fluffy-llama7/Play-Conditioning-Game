using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Enemy;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject target;
    [SerializeField] private float damage = 2.0f;
    private float force = 20f;
    private float timer = 0.0f;

    void Awake()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        rb.velocity = direction * force;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
