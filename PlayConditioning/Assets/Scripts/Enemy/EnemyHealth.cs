using System.Collections;
using UnityEngine;
using Mech;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 10.0f;
    private float currentHealth;
    public bool isEnclosed = false;

    private void Awake()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("Enemy's health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (isEnclosed)
        {
            TakeDamage(0.5f);
            isEnclosed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Orb")
        {
            float damage = collision.gameObject.GetComponent<OrbController>().GetDamage();
            TakeDamage(damage);
        }
    }
}
