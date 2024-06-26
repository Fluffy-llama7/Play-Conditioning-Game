using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float mechanicCooldown = 1.0f;
    private Rigidbody2D body;
    private float horizontal;
    private float vertical;
    private Vector3 direction;
    private IMechanic left1;
    private IMechanic right1;
    private IMechanic left2;
    private IMechanic right2;
    private IMechanic left3;
    private IMechanic right3;
    private bool isCooldownActive;
    private float cooldownTimer;

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
        isCooldownActive = false;
    }

    public void OnLeftClick(float version)
    {
        if (isCooldownActive) return;

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

        StartCooldown();
    }

    public void OnRightClick(float version)
    {
        if (isCooldownActive) return;

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

        StartCooldown();
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

        if (isCooldownActive)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                isCooldownActive = false;
            }
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

    private void StartCooldown()
    {
        isCooldownActive = true;
        cooldownTimer = mechanicCooldown;
    }
}
