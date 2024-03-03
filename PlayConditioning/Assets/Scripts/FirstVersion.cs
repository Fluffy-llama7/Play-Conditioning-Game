using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using NUnit.Framework;

public class FirstVersion : MonoBehaviour
{
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
    }

    public void Damage(GameObject enemy)
    {

    }

    public void OnLeftClick(GameObject player, string mechanic)
    {

    }

    public void OnRightClick(GameObject player, string mechanic)
    {

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Prevents player from shooting and swinging at the same time
        Assert.IsFalse(swinging && shooting);
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
