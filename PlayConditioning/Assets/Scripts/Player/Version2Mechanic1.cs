using Mech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Version2Mechanic1 : MonoBehaviour, IMechanic
{
    private GameObject orb;
    private GameObject player;
    private Rigidbody2D orbRigidbody;
    private bool isActive = false;
    [SerializeField] private float orbSpeed = 5.0f;

    // Mechanic 1: Summon an orb that follows the player

    private void Awake()
    {
        orb = GameObject.Find("Orb");
        player = GameObject.Find("Player");

        orbRigidbody = orb.GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
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
