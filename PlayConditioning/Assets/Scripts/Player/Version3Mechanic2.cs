using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class Version3Mechanic2 : MonoBehaviour, IMechanic
{
    private Vector2 mousePosition;
    [SerializeField] private GameObject prefab;
    private float force = 75f;
    private bool isCharging = false;
    private float chargeStartTime;
    private float maxChargeTime = 2f; // Faster charging

    // Mechanic 2: Charge and shoot a projectile

    public void Execute()
    {
        if (!isCharging)
        {
            // Start charging
            isCharging = true;
            chargeStartTime = Time.time;
            Debug.Log("Charging started");
        }
        else
        {
            // Shoot the projectile
            float chargeTime = Mathf.Min(Time.time - chargeStartTime, maxChargeTime);
            float chargeFactor = chargeTime / maxChargeTime;

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDirection = mousePosition - (Vector2)transform.position;
            shootDirection.Normalize();

            GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            // Adjust the projectile size based on charge time
            float size = Mathf.Lerp(1f, 5f, chargeFactor); // Increase size growth faster
            projectile.transform.localScale = new Vector3(size, size, 1f);

            // Apply force based on charge time
            projectileRb.AddForce(shootDirection * force * chargeFactor, ForceMode2D.Impulse);

            Debug.Log("Shoot with charge time: " + chargeTime + " seconds");

            // Reset charging state
            isCharging = false;
        }
    }

    public void Update()
    {
        
    }
}
