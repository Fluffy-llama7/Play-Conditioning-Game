using System.Collections;
using UnityEngine;
using Mech;
using Enemy;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private float totalHealth = 100.0f;
    private float currentHealth;

    private void Start()
    {
        this.currentHealth = this.totalHealth;
        this.healthBar.SetMaxHealth(this.totalHealth);
    }

    public float GetHealth()
    {
        return this.currentHealth;
    }

    public void SetHealth(float health)
    {
        this.currentHealth = health;
        this.healthBar.SetHealth(health);
    }

    public void TakeDamage(float damage)
    {
        this.currentHealth -= damage;
        this.healthBar.SetHealth(this.currentHealth);

        if (this.currentHealth <= 0)
        {
            Debug.Log("Player died");
            GameManager.instance.UpdateState(GameState.End);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
            this.TakeDamage(enemy.Damage);
        }
        else if (collision.gameObject.tag == "Enemy Projectile")
        {
            float damage = collision.gameObject.GetComponent<EnemyProjectile>().GetDamage();
            this.TakeDamage(damage);
        }

        if (GameManager.instance.GetVersion() == 2)
        {
            if (collision.gameObject.name == "Orb")
            {
                float damage = collision.gameObject.GetComponent<OrbController>().GetDamage();
                this.TakeDamage(damage);
            }
        }
    }
}