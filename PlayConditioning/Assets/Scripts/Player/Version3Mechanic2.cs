using System.Collections;
using UnityEngine;
using Mech;

public class Version3Mechanic2 : MonoBehaviour, IMechanic
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float force = 25f;
    [SerializeField] private float maxChargeTime = 5f;
    [SerializeField] private float minSize = 1f;
    [SerializeField] private float maxSize = 8f;
    private enum State { Charging, Launching }
    private State currentState = State.Charging;
    private Vector2 mousePosition;
    private GameObject currentProjectile = null;
    private float growStartTime;
    private float baseDamage = 1f;

    // Mechanic 2: Charge and shoot a projectile

    private void Awake()
    {
        if (GameManager.instance.GetVersion() != 3)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }
    }

    public void Execute()
    {
        switch (currentState)
        {
            case State.Charging:
                StartCharging();
                break;

            case State.Launching:
                LaunchProjectile();
                break;
        }
    }

    private void StartCharging()
    {
        // Create a new projectile and start charging
        currentProjectile = Instantiate(prefab, transform.position, Quaternion.identity);
        currentProjectile.transform.localScale = new Vector3(minSize, minSize, 1f);
        Collider2D projectileCollider = currentProjectile.GetComponent<Collider2D>();
        projectileCollider.enabled = false; // Disable collision initially
        growStartTime = Time.time;
        currentState = State.Launching;
        Debug.Log("Charging started");
    }

    private void LaunchProjectile()
    {
        if (currentProjectile == null) return;

        // Launch the projectile
        float chargeTime = Mathf.Min(Time.time - growStartTime, maxChargeTime);
        float chargeFactor = chargeTime / maxChargeTime;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = mousePosition - (Vector2)transform.position;
        shootDirection.Normalize();

        Rigidbody2D projectileRb = currentProjectile.GetComponent<Rigidbody2D>();

        projectileRb.AddForce(shootDirection * force, ForceMode2D.Impulse);

        // Enable the projectile's collider to interact with enemies
        Collider2D launchedProjectileCollider = currentProjectile.GetComponent<Collider2D>();
        launchedProjectileCollider.enabled = true;

        // Set the damage based on the projectile size
        float projectileSize = currentProjectile.transform.localScale.x;
        float damage = baseDamage * Mathf.Pow(projectileSize, 2); // Quadratic scaling for damage
        currentProjectile.GetComponent<PlayerProjectile>().SetDamage(damage);

        Debug.Log("Projectile launched with charge time: " + chargeTime + " seconds and damage: " + damage);

        // Prepare for the next projectile
        currentProjectile = null;
        currentState = State.Charging;
    }

    public void Update()
    {
        if (currentState == State.Launching && currentProjectile != null)
        {
            // Make the projectile follow the player while charging
            currentProjectile.transform.position = transform.position;

            float elapsedTime = Time.time - growStartTime;
            float growFactor = Mathf.Min(elapsedTime / maxChargeTime, 1f);
            float size = Mathf.Lerp(minSize, maxSize, growFactor);
            currentProjectile.transform.localScale = new Vector3(size, size, 1f);
        }
    }
}
