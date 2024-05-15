using System.Collections;
using UnityEngine;
using Mech;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] 
    private float blastDamage = 3.0f;
    [SerializeField] 
    private float totalHealth = 10.0f;
    [SerializeField]
    private float orbDamage = 10.0f;

    void TakeDamage(GameObject obj)
    {
        if (obj.name == "Blast")
        {
            totalHealth -= blastDamage;
        }

        if (obj.name == "Orb")
        {
            totalHealth -= orbDamage;
        }

        if (totalHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Blast" || collision.gameObject.name == "Orb")
        {
            TakeDamage(collision.gameObject);
        }
    }
}