using Mech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Version2Mechanic1 : MonoBehaviour, IMechanic
{
    private GameObject orb;
    private GameObject player;
    private Rigidbody2D orbRigidbody;
    private Collider2D orbCollider;
    private bool isActive = false;
    [SerializeField] private float orbSpeed = 5.0f;

    // Mechanic 1: player is followed by orb
    // Orb can also damage player upon contact
    // Orb can be guided to hit enemies

    private void Awake()
    {
        orb = GameObject.Find("Orb");
        player = GameObject.FindWithTag("Player");

        orbRigidbody = orb.GetComponent<Rigidbody2D>();
        orbCollider = orb.GetComponent<Collider2D>();

        enabled = false;
    }

    public void Execute()
    {
        enabled = true;
        isActive = true;
    }

    public void Update()
    {
        if (!isActive) return;

        Vector2 direction = (player.transform.position - orb.transform.position).normalized;
        orbRigidbody.velocity = direction * orbSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;

        if (collision.gameObject == player)
        {
            Debug.Log("Orb hit the player!");
        }
    }
}
