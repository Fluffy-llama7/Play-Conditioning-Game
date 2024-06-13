using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 1.0f;
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 30.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
    }

    public void SetDamage(float damageAmt)
    {
        damage = damageAmt;
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
