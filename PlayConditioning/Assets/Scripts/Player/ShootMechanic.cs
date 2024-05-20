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
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

        GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddForce(mousePosition * 10, ForceMode2D.Impulse);

        Debug.Log("Shoot");
    }

    public void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Stop()
    {

    }
}