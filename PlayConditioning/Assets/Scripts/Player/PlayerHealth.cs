using System.Collections;
using UnityEngine;
using Mech;
using Unity.VisualScripting;
using Enemy;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] 
    private float totalHealth = 100.0f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = totalHealth;
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("Player's health: " + currentHealth);

        GameManager.instance.UpdateHealth(currentHealth, totalHealth);

        if (currentHealth <= 0)
        {
            GameManager.instance.UpdateState(GameState.End);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
            TakeDamage(enemy.Damage);
        }
        else if (collision.gameObject.tag == "Enemy Projectile")
        {
            float damage = collision.gameObject.GetComponent<EnemyProjectile>().Damage;
            TakeDamage(damage);
        }
    }
}