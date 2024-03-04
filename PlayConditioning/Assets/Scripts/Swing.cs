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
    private bool swing;

    void Awake()
    {
        rope = GameObject.Find("Rope");

        ball = GameObject.Find("Ball");
        hinge = ball.GetComponent<HingeJoint2D>();
        rb = ball.GetComponent<Rigidbody2D>();

        hinge.enabled = false;
        swing = false;
    }

    public void Execute()
    {
        swing = !swing;

        if (swing)
        {
            // Connects the ball to the rope
            hinge.enabled = true;
            hinge.connectedBody = rope.GetComponent<Rigidbody2D>();
            hinge.autoConfigureConnectedAnchor = false;
            hinge.useConnectedAnchor = true;

            // Uses the motor to swing the ball
            hinge.useMotor = true;
            motor = hinge.motor;
            motor.motorSpeed = 200.0f;
            motor.maxMotorTorque = 1000.0f;
            hinge.motor = motor;
        }
        else
        {
            Stop();
        }
    }

    public void Update()
    {

    }

    public void Stop()
    {
        // Reset variables
        hinge.enabled = false;
        hinge.connectedBody = null;
        hinge.useMotor = false;

        rb.rotation = 0.0f;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
    }
}
