using Mech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Version2Mechanic1 : MonoBehaviour, IMechanic
{
    [SerializeField] private float orbSpeed = 5.0f;
    [SerializeField] private float duration = 5.0f;
    private GameObject orb;
    private GameObject player;
    private Rigidbody2D orbRigidbody;
    private bool isActive = false;
    private float activeTime;

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
        activeTime = Time.time + duration;
    }

    public void Update()
    {
        if (!isActive) return;

        if (Time.time > activeTime)
        {
            isActive = false;
            orbRigidbody.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = (player.transform.position - orb.transform.position).normalized;
        orbRigidbody.velocity = direction * orbSpeed;
    }
}
