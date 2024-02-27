using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mech
{
    public class SwingBall : MonoBehaviour
    {
        [SerializeField] private float speed = 100.0f;
        [SerializeField] private float torque = 1000.0f;
        private GameObject player;
        private GameObject ball;
        private LineRenderer rope;
        private HingeJoint2D ropeHinge;
        private HingeJoint2D ballHinge;
        private JointMotor2D ropeMotor;

        void Awake()
        {
            player = GameObject.Find("Player");

            ball = GameObject.Find("Ball");
            ballHinge = ball.GetComponent<HingeJoint2D>();

            rope = GameObject.Find("Rope").GetComponent<LineRenderer>();
            ropeHinge = rope.GetComponent<HingeJoint2D>();
        }

        void Update()
        {
            Swing();
        }

        public void Swing()
        {
            // Set rope to attach ball to player
            rope.SetPositions(new Vector3[] { player.transform.position, ball.transform.position });

            // Set up hinge joint to attach ball to rope;
            ballHinge.connectedBody = rope.GetComponent<Rigidbody2D>();
            ballHinge.autoConfigureConnectedAnchor = true;

            // Set up hinge joint to attach rope to player
            ropeHinge.connectedBody = player.GetComponent<Rigidbody2D>();
            ropeHinge.autoConfigureConnectedAnchor = true;

            // Set up motor to swing ball
            ropeHinge.useMotor = true;
            ropeMotor.motorSpeed = speed;
            ropeMotor.maxMotorTorque = torque;
            ropeHinge.motor = ropeMotor;

            // FIXME: rope and ball speed are not the same
        }
    }
} 