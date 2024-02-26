using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Mech;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Vector2 = UnityEngine.Vector2;

public class Version2: MonoBehaviour, IMechanic
{
    [SerializeField] GameObject ball;
    private SpriteRenderer ballRenderer;
    private CircleCollider2D ballCollider;
    
    // not needed?
    [SerializeField] MonoBehaviour playerScript;

    // not needed?
    // [SerializeField] GameObject enemy;

    private bool ballFollowActive = false;
    private bool ballTargetActive = false;
    private float timeSinceLastFall = 0.0f;
    [SerializeField] private float timeBetweenOrbs = 3.0f;
    private Vector2 ballSize;
    private Color ballOriginalColor;

    public void Damage(GameObject enemy)
    {
        // Not sure what this function does.
        // It would be easier to handle the damage through the collider system

    }
   
    // Mechanic 1: player is followed by orb
    //  Orb can also damage player upon contact
    //  Orb can be guided to hit enemies
    public void OnLeftClick(GameObject player, string mechanic)
    {
        // Both mechanics are toggled
        ballFollowActive = true;
        ballTargetActive = false;
        resetLocalVariables();
    }

    // Mechanic 2: orbs target position of player every few seconds
    //  Every few seconds, orb falls on previous player position
    //  Orbs damage both enemies and player
    public void OnRightClick(GameObject player, string mechanic)
    {
        ballFollowActive = false;
        ballTargetActive = true;
        resetLocalVariables();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set up local variables, get initial values
        ball = GameObject.Find("Ball");
        playerScript = GetComponent<Player>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
        ballRenderer = ball.GetComponent<SpriteRenderer>();
        ballOriginalColor = ballRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        // debug, i assume OnLeftClick/OnRightClick is called by another script?
        // if (Input.GetButtonDown("Fire1"))OnLeftClick(null, null);
        // if (Input.GetButtonDown("Fire2"))OnRightClick(null, null);
        // Both mechanics cannot be active at once.
        Assert.IsFalse(ballTargetActive && ballFollowActive);
        if (ballFollowActive)
        {
            // Mechanic 1 Update Code
            ball.transform.position = Vector2.MoveTowards(ball.transform.position, this.transform.position, 5.0f * Time.deltaTime);
            
        }
        if (ballTargetActive)
        {  
            // Mechanic 2 Update Code
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
    void OnDisable()
    {
        resetLocalVariables();
    }
    void resetLocalVariables()
    {
        ball.transform.localScale = new Vector2(2.4452f, 2.4452f);
        timeSinceLastFall = 0.0f;
        ballRenderer.color = ballOriginalColor;
        ballCollider.enabled = true;
    }
}
