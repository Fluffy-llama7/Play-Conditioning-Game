using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    private Vector3 direction;
    public Animator animator;
    public float speed = 10.0f;
    public float attackDelay = 3.0f;
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
        left1 = GetComponent<Version1Mechanic1>();
        right1 = GetComponent<Version1Mechanic2>();
        left2 = GetComponent<Version2Mechanic1>();
        right2 = GetComponent<Version2Mechanic2>();
        left3 = GetComponent<Version3Mechanic1>();
        right3 = GetComponent<Version3Mechanic2>();
        animator = GetComponent<Animator>();
    }

    public void OnLeftClick(float version)
    {
        switch (version)
        {
            case 1:
                left1.Execute();
                break;
            case 2:
                left2.Execute();
                break;
            case 3:
                left3.Execute();
                break;
        }
    }

    public void OnRightClick(float version)
    {
        switch (version)
        {
            case 1:
                right1.Execute();
                break;
            case 2:
                right2.Execute();
                break;
            case 3:
                right3.Execute();
                break;
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical, 0).normalized;

        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);

        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClick(GameManager.instance.GetVersion());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnRightClick(GameManager.instance.GetVersion());
        }
    }

    public float GetCurrentSpeed()
    {
        return speed;
    }

    public Vector3 GetMovementDirection()
    {
        return direction;
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}