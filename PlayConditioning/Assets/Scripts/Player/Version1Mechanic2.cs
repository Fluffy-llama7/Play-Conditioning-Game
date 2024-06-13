using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class Version1Mechanic2 : MonoBehaviour, IMechanic
{
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    private GameObject orb;
    private GameObject player;
    private Rigidbody2D rb;
    private float angle;
    private bool active;
    
    // Mechanic 2: Orb swings around the player

    void Awake()
    {
        if (GameManager.instance.GetVersion() != 1)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }

        orb = GameObject.Find("Orb");
        player = GameObject.Find("Player");
        rb = orb.GetComponent<Rigidbody2D>();

        angle = 0.0f;
        this.active = false;
    }

    public void Execute()
    {
        // If the player is not swinging, start swinging
        if (!this.active)
        {
            this.active = true;
        }
        else
        {
            // If player left clicks while swinging, stop swinging
            Stop();
        }
    }

    public void Update()
    {
        // If active, then the player is swinging
        if (this.active)
        {
            var distance = Vector3.Distance(orb.transform.position, player.transform.position);

            // If the ball is outside the radius, snap it back to the radius
            if (distance > radius)
            {
                SnapToRadius();
            }

            // Debug.DrawLine(rope.transform.position, ball.transform.position, Color.red);

            // Swinging logic
            orb.transform.RotateAround(player.transform.position, Vector3.forward, speed * Time.deltaTime);
            
            angle += speed * Time.deltaTime;

            // If the player made full rotation, stop swinging
            if (angle >= 360.0f)
            {
                Stop();
            }
        }
    }

    public void Stop()
    {
        // Resets local variables
        this.active = false;

        angle = 0.0f;
        orb.transform.rotation = Quaternion.identity;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
    }

    public bool IsActive()
    {
        return this.active;
    }

    private void SnapToRadius()
    {
        // Calculate the direction vector from rope to ball and normalize it
        Vector3 direction = (orb.transform.position - player.transform.position).normalized;

        // Move the ball to the correct position on the radius
        orb.transform.position = player.transform.position + direction * radius;
    }
}
