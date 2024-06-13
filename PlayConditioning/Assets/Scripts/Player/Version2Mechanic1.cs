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
    private float activeTime;
    [SerializeField] private float orbSpeed = 5.0f;
    [SerializeField] private float duration = 5.0f; // Duration the mechanic will last

    // Mechanic 1: Summon an orb that follows the player

    private void Awake()
    {
        if (GameManager.instance.GetVersion() != 2)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }

        orb = GameObject.Find("Orb");
        player = GameObject.Find("Player");

        orbRigidbody = orb.GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        isActive = true;
        activeTime = Time.time + duration; // Set the time when the mechanic will end
    }

    public void Update()
    {
        if (!isActive) return;

        if (Time.time > activeTime)
        {
            isActive = false; // Deactivate the mechanic after the duration
            orbRigidbody.velocity = Vector2.zero; // Stop the orb movement
            return;
        }

        Vector2 direction = (player.transform.position - orb.transform.position).normalized;
        orbRigidbody.velocity = direction * orbSpeed;
    }
}
