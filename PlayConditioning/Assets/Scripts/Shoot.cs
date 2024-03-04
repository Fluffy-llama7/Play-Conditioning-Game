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
    private GameObject ball;
    private Rigidbody2D rb;
    private GameObject gun;

    void Awake()
    {
        gun = GameObject.Find("Gun");

        ball = GameObject.Find("Ball");
        rb = ball.GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0.0f;
        
        // Shoots a bullet from the player's position
        Instantiate(bullet, gun.transform.position, Quaternion.identity);
    }

    public void Update()
    {
    
    }
}
