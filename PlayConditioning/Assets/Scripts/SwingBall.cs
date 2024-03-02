using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class SwingBall : ScriptableObject
{
    private Rigidbody2D body;
    private GameObject ball;
    private HingeJoint2D hinge;
    private JointMotor2D motor;

    void Awake()
    {
        ball = GameObject.Find("Ball");
        hinge = ball.GetComponent<HingeJoint2D>();

        body = ball.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Swings the ball
    /// </summary>
    /// <param name="player"> player that is swinging the ball </param>
    public void Execute(GameObject player)
    {
        body.bodyType = RigidbodyType2D.Dynamic;

        hinge.enabled = true;
        hinge.connectedBody = player.GetComponent<Rigidbody2D>();
        hinge.autoConfigureConnectedAnchor = false;

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
        body.velocity = Vector2.zero;
        body.bodyType = RigidbodyType2D.Kinematic;
    }
}
