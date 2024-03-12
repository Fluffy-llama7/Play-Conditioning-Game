using System.Collections;
using UnityEngine;
using Mech;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] 
    private float blastDamage = 3.0f;
    [SerializeField] 
    private float enemyDamage = 3.0f;
    [SerializeField] 
    private float totalHealth = 20.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Blast")
        {
            totalHealth -= blastDamage;
        }
        else if (collision.gameObject.name == "Blast")
        {
            totalHealth -= enemyDamage;
        }
    }
}