using UnityEngine;
using Mech;

public class Version2Mechanic2 : MonoBehaviour, IMechanic
{
    [SerializeField] private float timeBetweenOrbs = 3.0f;

    private GameObject orb;
    private SpriteRenderer orbRenderer;
    private CircleCollider2D orbCollider;
    private Color orbOriginalColor;
    private Vector3 orbOriginalSize;
    private float timeSinceLastFall = 0.0f;
    private bool active = false;
    private bool orbGrowing = false;
    private Vector3 lastPlayerPosition;

    private void Awake()
    {
        orb = GameObject.Find("Orb");
        if (orb != null)
        {
            orbCollider = orb.GetComponent<CircleCollider2D>();
            orbRenderer = orb.GetComponent<SpriteRenderer>();
            orbOriginalColor = orbRenderer.color;
            orbOriginalSize = orb.transform.localScale;
        }
    }

    public void Execute()
    {
        if (orb == null) return;

        active = true;
        ResetOrb();
        // Capture the player's last position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            lastPlayerPosition = player.transform.position;
            orb.transform.position = lastPlayerPosition;
            orbGrowing = true;
            timeSinceLastFall = 0.0f;
        }
    }

    public void Update()
    {
        if (!active || !orbGrowing || orb == null) return;

        timeSinceLastFall += Time.deltaTime;

        if (timeSinceLastFall < timeBetweenOrbs)
        {
            // Grow the orb in size and change color to grey
            orbRenderer.color = Color.grey;
            Vector3 orbSize = orbOriginalSize * Mathf.Clamp01(timeSinceLastFall / timeBetweenOrbs);
            orb.transform.localScale = orbSize;
            orbCollider.enabled = false; // Disable collider during this phase
        }
        else
        {
            // Once the orb is fully grown and the time has elapsed, revert to original color and enable collider
            orbRenderer.color = orbOriginalColor;
            orb.transform.localScale = orbOriginalSize;
            orbCollider.enabled = true; // Enable collider once the orb is in position
            orbGrowing = false;
            active = false; // Deactivate to ensure the process happens once per Execute call
        }
    }

    public void Disable()
    {
        active = false;
        orbGrowing = false;
        ResetOrb();
    }

    private void OnDisable()
    {
        ResetOrb();
    }

    private void ResetOrb()
    {
        if (orb != null)
        {
            orb.transform.localScale = orbOriginalSize;
            orbRenderer.color = orbOriginalColor;
            orbCollider.enabled = false; // Ensure collider is disabled until the orb is active again
        }
    }
}
