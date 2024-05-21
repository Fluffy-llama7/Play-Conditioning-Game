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
    GameObject orb;
    private SpriteRenderer orbRenderer;
    private CircleCollider2D orbCollider;
    private Color orbOriginalColor;
    private Version2Mechanic2 otherMechanic;
    [SerializeField] float orbSpeed = 5.0f;

    private bool active = false;


    // Mechanic 1: player is followed by orb
    //  Orb can also damage player upon contact
    //  Orb can be guided to hit enemies
    public void Awake(){
        orb = GameObject.Find("Orb");
        orbCollider = orb.GetComponent<CircleCollider2D>();
        orbRenderer = orb.GetComponent<SpriteRenderer>();
        orbOriginalColor = orbRenderer.color;
        otherMechanic = GetComponent<Version2Mechanic2>();
    }
    public void Execute(){
        active = true;
        otherMechanic.Disable();
        resetLocalVariables();
    }

    public void Update(){
        if (active){
            orb.transform.position = Vector2.MoveTowards(orb.transform.position, this.transform.position, orbSpeed * Time.deltaTime);
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