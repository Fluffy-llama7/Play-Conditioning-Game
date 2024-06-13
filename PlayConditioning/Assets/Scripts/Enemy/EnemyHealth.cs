using System.Collections;
using UnityEngine;
using Mech;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] HealthBarController healthBar;
    [SerializeField] private float totalHealth = 10.0f;
    private float currentHealth;

    private void Awake()
    {
        this.currentHealth = totalHealth;
        this.healthBar.SetMaxHealth(totalHealth);
    }

    private void OnDestroy()
    {
        EnemyManager.instance.EnemyKilled();
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        healthBar.SetHealth(health);
    }

    public void TakeDamage(float damage)
    {
        this.currentHealth -= damage;
        this.healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Orb")
        {
            float damage = collision.gameObject.GetComponent<OrbController>().GetDamage();
            TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            float damage = collision.gameObject.GetComponent<PlayerProjectile>().GetDamage();
            TakeDamage(damage);
        }
    }
}
