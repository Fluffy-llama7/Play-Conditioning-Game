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
        left1 = GetComponent<ShootMechanic>();
        right1 = GetComponent<SwingMechanic>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(GameObject enemy)
    {

    }

    public void OnLeftClick(string mechanic)
    {

    }

    public void OnRightClick(string mechanic)
    {

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical, 0).normalized;

        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            left1.Execute();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            right1.Execute();
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