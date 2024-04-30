using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    public float speed = 10.0f;
    private IMechanic left1;
    private IMechanic right1;
    private IMechanic left2;
    private IMechanic right2;
    private IMechanic left3;
    private IMechanic right3;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        left1 = GetComponent<ShootMechanic>();
        right1 = GetComponent<SwingMechanic>();
    }

    public void Damage(GameObject enemy)
    {

    }

    public void OnLeftClick(string mechanic)
    {
        if (mechanic == "Shoot")
        {
            left1.Execute();
        }
    }

    public void OnRightClick(string mechanic)
    {
        if (mechanic == "Swing")
        {
            right1.Execute();
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClick("Shoot");
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick("Swing");
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}