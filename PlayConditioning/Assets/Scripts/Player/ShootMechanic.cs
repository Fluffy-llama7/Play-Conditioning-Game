using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using System.Threading;
using UnityEngine.XR;
using Unity.VisualScripting;

public class ShootMechanic : MonoBehaviour, IMechanic
{
    [SerializeField] private GameObject bullet;
    private GameObject gun;
    private SwingMechanic swing;
    private GameObject orb;
    private Rigidbody2D rb;

    void Awake()
    {
        gun = GameObject.Find("Gun");
        orb = GameObject.Find("Orb");
        rb = orb.GetComponent<Rigidbody2D>();
        swing = GetComponent<SwingMechanic>();
    }

    public void Execute()
    {
        if (swing.IsActive())
        {
            Stop();
        }

        Instantiate(bullet, gun.transform.position, Quaternion.identity);
    }

    public void Update()
    {

    }

    public void Stop()
    {
        // Reset local variables
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
    }
}