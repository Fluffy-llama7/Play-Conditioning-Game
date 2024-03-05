using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class Swing : MonoBehaviour, IMechanic
{
    private GameObject ball;
    private GameObject rope;
    private Rigidbody2D rb;
    private HingeJoint2D hinge;
    private JointMotor2D motor;
    private bool active;

    void Awake()
    {
        rope = GameObject.Find("Rope");

        ball = GameObject.Find("Ball");
        hinge = ball.GetComponent<HingeJoint2D>();
        rb = ball.GetComponent<Rigidbody2D>();

        hinge.enabled = false;
        active = false;
    }

    public void Execute()
    {
        if (!active)
        {
            active = true;
        }
        else
        {
            Stop();
        }
    }

    public void Update()
    {
        if (active)
        {
            // Connects the ball to the rope
            hinge.enabled = true;
            hinge.connectedBody = rope.GetComponent<Rigidbody2D>();
            hinge.autoConfigureConnectedAnchor = false;
            hinge.useConnectedAnchor = true;

            // Uses the motor to swing the ball
            hinge.useMotor = true;
            motor = hinge.motor;
            motor.motorSpeed = 700.0f;
            motor.maxMotorTorque = 1000.0f;
            hinge.motor = motor;

            if (Mathf.Abs(rb.rotation) >= 420.0f)
            {
                Stop();
            }
        }
    }

    public void Stop()
    {
        active = false;

        // Reset variables
        hinge.enabled = false;
        hinge.connectedBody = null;
        hinge.useMotor = false;

        rb.rotation = 0.0f;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
    }
}
