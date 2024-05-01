using Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BossEnemyController : MonoBehaviour, IEnemy
{
    private GameObject target;
    private Rigidbody2D rb;

    public float health = 100f;
    public float speed = 5f;

    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Vector3 direction = (target.transform.position - transform.position);
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        
    }

    public void Attack()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ball") 
        {
            TakeDamage();
        }
    }
}