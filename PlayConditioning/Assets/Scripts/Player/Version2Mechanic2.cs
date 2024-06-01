using UnityEngine;
using Mech;

public class Version2Mechanic2 : MonoBehaviour, IMechanic
{
    [SerializeField] private float timeBetweenOrbs = 3.0f;

    private GameObject orb;
    private SpriteRenderer orbRenderer;
    private CircleCollider2D orbCollider;
    private Color orbOriginalColor;
    private Vector2 orbSize;
    private float timeSinceLastFall = 0.0f;
    private bool active = false;

    private void Awake()
    {
        orb = GameObject.Find("Orb");
        orbCollider = orb.GetComponent<CircleCollider2D>();
        orbRenderer = orb.GetComponent<SpriteRenderer>();
        orbOriginalColor = orbRenderer.color;
    }

    public void Execute()
    {
        active = true;

        orbCollider.sharedMaterial.friction = 0f;
        orbCollider.sharedMaterial.bounciness = 0f;

        ResetOrb();
    }

    public void Update()
    {
        if (active)
        {
            orbRenderer.color = Color.grey;
            timeSinceLastFall += Time.deltaTime;
            orbSize = new Vector2(2.4452f, 2.4452f) * Mathf.Clamp01(timeSinceLastFall / timeBetweenOrbs);
            orb.transform.localScale = orbSize;

            if (timeSinceLastFall > timeBetweenOrbs)
            {
                orbRenderer.color = orbOriginalColor;
                orbCollider.enabled = true;
            }

            if (timeSinceLastFall > timeBetweenOrbs + 0.25f)
            {
                timeSinceLastFall = 0.0f;
                orbCollider.enabled = false;
                orb.transform.position = transform.position;
            }
        }
    }

    public void Disable()
    {
        active = false;
    }

    private void OnDisable()
    {
        ResetOrb();
    }

    private void ResetOrb()
    {
        orb.transform.localScale = new Vector2(2.4452f, 2.4452f);
        orbRenderer.color = orbOriginalColor;
        orbCollider.enabled = true;
    }
}
