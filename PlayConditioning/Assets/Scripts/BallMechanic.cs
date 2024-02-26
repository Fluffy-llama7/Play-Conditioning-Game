using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mech
{
    public class BallMechanic : MonoBehaviour, IMechanic
    {
        [SerializeField] private float speed = 1000.0f;
        [SerializeField] private float torque = 10000.0f;
        private GameObject ball;
        private LineRenderer line;
        private HingeJoint2D joint;
        private JointMotor2D motor;

        void Awake()
        {
            joint = gameObject.AddComponent<HingeJoint2D>();
            joint.enabled = false;

            ball = GameObject.Find("Ball");

            // Disable line renderer until mechanic 2 is activated
            line = GetComponent<LineRenderer>();
            line.enabled = false;
        }

        void Update()
        {
            // Testing to see if mechanic 2 is working
            if (Input.GetMouseButtonDown(1))
            {
                OnRightClick(gameObject, "BallMechanic");
            }
        }

        public void Damage(GameObject enemy)
        {
            // Assuming TakeDamage is a function in the enemy script (similar to how exercise 4 is set up)
        }

        public void OnLeftClick(GameObject player, string mechanic)
        {

        }

        public void OnRightClick(GameObject player, string mechanic)
        {
            SwingBall(player);
        }

        // <summary>
        // Call this function to swing ball
        // </summary>
        public void SwingBall(GameObject player)
        {
            // Enable line renderer to draw string from player to ball
            line.enabled = true;
            line.SetPositions(new Vector3[] { player.transform.position, ball.transform.position });
            Debug.Log("Drawing line");

            // Create hinge joint to attach the ball to the player
            joint.enabled = true;
            joint.connectedBody = ball.GetComponent<Rigidbody2D>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = Vector2.zero;
            Debug.Log("Attaching ball to player");

            // Set up motor to swing the ball
            joint.useMotor = true;
            motor = joint.motor;
            motor.motorSpeed = speed;
            motor.maxMotorTorque = torque;
            joint.motor = motor;
            Debug.Log($"Motor speed: {joint.motor.motorSpeed}, Motor torque: {joint.motor.maxMotorTorque}");

        }
    }
} 