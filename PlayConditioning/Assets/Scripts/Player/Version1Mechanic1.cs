using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class Version1Mechanic1 : MonoBehaviour, IMechanic
{
    [SerializeField] private GameObject prefab;
    private Vector2 mousePosition;
    private float force = 20f;

    // Mechanic 1: Player shoots a projectile in the direction of the mouse
    private void Awake()
    {
        if (GameManager.instance.GetVersion() != 1)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }
    }

    public void Execute()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = mousePosition - (Vector2)transform.position;
        shootDirection.Normalize();

        GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        projectileRb.AddForce(shootDirection * force, ForceMode2D.Impulse);
    }

    public void Update()
    {
        
    }
}