using System.Collections;
using UnityEngine;
using Mech;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] 
    private float blastDamage = 3.0f;
    [SerializeField] 
    private float totalHealth = 20.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Blast" || collision.gameObject.name == "Orb")
        {
            totalHealth -= blastDamage;
        }
    }
}