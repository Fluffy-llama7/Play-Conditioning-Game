using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Mech;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Vector2 = UnityEngine.Vector2;

public class Version2Mechanic2: MonoBehaviour, IMechanic
{
    GameObject ball;
    private SpriteRenderer ballRenderer;
    private CircleCollider2D ballCollider;
    private Vector2 ballSize;
    private Color ballOriginalColor;
    private float timeSinceLastFall = 0.0f;
    [SerializeField] private float timeBetweenOrbs = 3.0f;
    private bool active = false;
    private Version2Mechanic1 otherMechanic;


    // Mechanic 2: orbs target position of player every few seconds
    //  Every few seconds, orb falls on previous player position
    //  Orbs damage both enemies and player
    public void Awake(){
        ball = GameObject.Find("Ball");
        ballCollider = ball.GetComponent<CircleCollider2D>();
        ballRenderer = ball.GetComponent<SpriteRenderer>();
        ballOriginalColor = ballRenderer.color;
        otherMechanic = GetComponent<Version2Mechanic1>();
    }
    public void Execute(){
        active = true;
        otherMechanic.Disable();
        resetLocalVariables();
    }

    public void Update(){
        if (active){
            ballRenderer.color = Color.grey;
            timeSinceLastFall += Time.deltaTime;
            ballSize = new Vector2(2.4452f, 2.4452f) * Mathf.Clamp(timeSinceLastFall/timeBetweenOrbs, 0, 1);
            ball.transform.localScale = ballSize;
            if (timeSinceLastFall > timeBetweenOrbs)
            {
                // Damage phase
                ballRenderer.color = ballOriginalColor;
                ballCollider.enabled = true;
            }
            if (timeSinceLastFall > timeBetweenOrbs+0.25f)
            {
                // End of damage phase
                timeSinceLastFall = 0.0f;
                ballCollider.enabled = false;
                ball.transform.position = this.transform.position;
            }
        }
        
    }
    public void Disable(){
        active = false;
    }
    void OnDisable(){
        resetLocalVariables();
    }
    void resetLocalVariables()
    {
        ball.transform.localScale = new Vector2(2.4452f, 2.4452f);
        ballRenderer.color = ballOriginalColor;
        ballCollider.enabled = true;
    }
}