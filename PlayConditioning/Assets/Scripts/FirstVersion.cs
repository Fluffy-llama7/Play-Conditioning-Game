using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using NUnit.Framework;

public class FirstVersion : MonoBehaviour, IMechanic
{
    private ShootBall shoot;
    private SwingBall swing;
    private Rigidbody2D body;
    private float horizontal;
    private float vertical;
    public float speed = 10.0f;
    private bool swinging = false;
    private bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        shoot = ScriptableObject.CreateInstance<ShootBall>();
        swing = ScriptableObject.CreateInstance<SwingBall>();
    }

    public void Damage(GameObject enemy)
    {

    }

    public void OnLeftClick(GameObject player, string mechanic)
    {
        if (mechanic == "shoot")
        {
            swing.Stop();
            shoot.Execute(player);
        }
    }

    public void OnRightClick(GameObject player, string mechanic)
    {
        if (mechanic == "swing")
        {
            shoot.Stop();
            swing.Execute(player);
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Prevents player from shooting and swinging at the same time
        Assert.IsFalse(swinging && shooting);

        // If left click is pressed, stop swinging and start shooting
        if (Input.GetMouseButtonDown(0))
        {
            swinging = false;
            shooting = true;
            OnLeftClick(gameObject, "shoot");
        }
        // If right click is pressed, stop shooting and start swinging
        else if (Input.GetMouseButtonDown(1))
        {
            shooting = false;
            swinging = true;
            OnRightClick(gameObject, "swing");
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
