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
    GameObject orb;
    private SpriteRenderer orbRenderer;
    private CircleCollider2D orbCollider;
    private Vector2 orbSize;
    private Color orbOriginalColor;
    private float timeSinceLastFall = 0.0f;
    [SerializeField] private float timeBetweenOrbs = 3.0f;
    private bool active = false;
    private Version2Mechanic1 otherMechanic;


    // Mechanic 2: orbs target position of player every few seconds
    //  Every few seconds, orb falls on previous player position
    //  Orbs damage both enemies and player
    public void Awake(){
        orb = GameObject.Find("Orb");
        orbCollider = orb.GetComponent<CircleCollider2D>();
        orbRenderer = orb.GetComponent<SpriteRenderer>();
        orbOriginalColor = orbRenderer.color;
        otherMechanic = GetComponent<Version2Mechanic1>();
    }
    public void Execute(){
        active = true;
        otherMechanic.Disable();
        resetLocalVariables();
    }

    public void Update(){
        if (active){
            orbRenderer.color = Color.grey;
            timeSinceLastFall += Time.deltaTime;
            orbSize = new Vector2(2.4452f, 2.4452f) * Mathf.Clamp(timeSinceLastFall/timeBetweenOrbs, 0, 1);
            orb.transform.localScale = orbSize;
            if (timeSinceLastFall > timeBetweenOrbs)
            {
                // Damage phase
                orbRenderer.color = orbOriginalColor;
                orbCollider.enabled = true;
            }
            if (timeSinceLastFall > timeBetweenOrbs+0.25f)
            {
                // End of damage phase
                timeSinceLastFall = 0.0f;
                orbCollider.enabled = false;
                orb.transform.position = this.transform.position;
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
        orb.transform.localScale = new Vector2(2.4452f, 2.4452f);
        orbRenderer.color = orbOriginalColor;
        orbCollider.enabled = true;
    }
}