using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Enemy;
using UnityEngine.UIElements;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 2.0f;
    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
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
