using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using System.Diagnostics;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    private Vector3 direction;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float speed = 10.0f;
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