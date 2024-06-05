using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float damage = 2.0f;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    public float GetDamage()
    {
        return damage;
    }
}
