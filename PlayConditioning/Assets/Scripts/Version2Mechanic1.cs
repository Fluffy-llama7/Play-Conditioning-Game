using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Mech;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Vector2 = UnityEngine.Vector2;

public class Version2Mechanic1: MonoBehaviour, IMechanic
{
    GameObject ball;
    private SpriteRenderer ballRenderer;
    private CircleCollider2D ballCollider;
    private Color ballOriginalColor;
    private Version2Mechanic2 otherMechanic;
    [SerializeField] float ballSpeed = 5.0f;

    private bool active = false;


    // Mechanic 1: player is followed by orb
    //  Orb can also damage player upon contact
    //  Orb can be guided to hit enemies
    public void Awake(){
        ball = GameObject.Find("Ball");
        ballCollider = ball.GetComponent<CircleCollider2D>();
        ballRenderer = ball.GetComponent<SpriteRenderer>();
        ballOriginalColor = ballRenderer.color;
        otherMechanic = GetComponent<Version2Mechanic2>();
    }
    public void Execute(){
        active = true;
        otherMechanic.Disable();
        resetLocalVariables();
    }

    public void Update(){
        if (active){
            ball.transform.position = Vector2.MoveTowards(ball.transform.position, this.transform.position, ballSpeed * Time.deltaTime);
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