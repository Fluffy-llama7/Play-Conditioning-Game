using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using System.Threading;
using UnityEngine.XR;
using Unity.VisualScripting;

public class Shoot : MonoBehaviour, IMechanic
{
    [SerializeField] private GameObject bullet;
    private GameObject gun;
    private Swing swing;
    private GameObject ball;
    private Rigidbody2D rb;

    void Awake()
    {
        gun = GameObject.Find("Gun");
        ball = GameObject.Find("Ball");
        rb = ball.GetComponent<Rigidbody2D>();
        swing = GetComponent<Swing>();
    }

    public void Execute()
    {
        if (swing.IsActive())
        {
            swing.Stop();
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
