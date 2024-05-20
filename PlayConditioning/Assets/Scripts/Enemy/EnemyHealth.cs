using System.Collections;
using UnityEngine;
using Mech;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] 
    private float totalHealth = 10.0f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = totalHealth;
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("Enemy's health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "Orb")
        {
            float damage = collision.gameObject.GetComponent<OrbController>().Damage;
            TakeDamage(damage);
        }
    }
}