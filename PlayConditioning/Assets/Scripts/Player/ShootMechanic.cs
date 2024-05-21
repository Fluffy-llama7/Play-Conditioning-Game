using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using System.Threading;
using UnityEngine.XR;
using Unity.VisualScripting;

public class ShootMechanic : MonoBehaviour, IMechanic
{
    private Vector2 mousePosition;
    [SerializeField] private GameObject prefab;
    [SerializeField]
    private float force = 20f;


    void Awake()
    {
    }

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

    public void Stop()
    {

    }
}