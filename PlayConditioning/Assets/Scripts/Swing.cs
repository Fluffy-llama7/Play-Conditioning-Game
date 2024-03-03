using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class Swing : MonoBehaviour, IMechanic
{

    private GameObject rope;
    private Rigidbody2D rb;
    private GameObject ball;
    private HingeJoint2D hinge;
    private JointMotor2D motor;

    void Awake()
    {
        rope = GameObject.Find("Rope");

        ball = GameObject.Find("Ball");
        hinge = ball.GetComponent<HingeJoint2D>();
        rb = ball.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Execute();
        }
        else
        {
            Stop();
        }
    }

    /// <summary>
    /// Swings the ball
    /// </summary>
    public void Execute()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;

        hinge.enabled = true;
        hinge.connectedBody = rope.GetComponent<Rigidbody2D>();
        hinge.autoConfigureConnectedAnchor = false;
        hinge.useConnectedAnchor = true;

        hinge.useMotor = true;
        motor = hinge.motor;
        motor.motorSpeed = 200;
        motor.maxMotorTorque = 1000;
        hinge.motor = motor;
    }

    /// <summary>
    /// Stops the ball from swinging
    /// </summary>
    public void Stop()
    {
        hinge.enabled = false;
        hinge.connectedBody = null;
        hinge.useMotor = false;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
