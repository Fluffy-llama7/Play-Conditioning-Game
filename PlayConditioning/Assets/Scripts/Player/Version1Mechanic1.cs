using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class Version1Mechanic1 : MonoBehaviour, IMechanic
{
    private Vector2 mousePosition;
    [SerializeField] private GameObject prefab;
    private float force = 20f;

    // Mechanic 1: Player shoots a projectile in the direction of the mouse
    public void Execute()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = mousePosition - (Vector2)transform.position;
        shootDirection.Normalize();

        GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        projectileRb.AddForce(shootDirection * force, ForceMode2D.Impulse);

        Debug.Log("Shoot");
    }

    public void Update()
    {
        
    }
}