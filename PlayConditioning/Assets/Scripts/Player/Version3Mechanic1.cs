using Mech;
using System.Collections.Generic;
using UnityEngine;

public class Version3Mechanic1 : MonoBehaviour, IMechanic
{
    public float recordTime = 5.0f;
    public float cooldownTime = 2.0f;
    private LineRenderer lineRenderer;
    private List<Vector3> positions;
    private float timer;
    private bool isActive = false;

    // Mechanic 1: Draw a path and enclose enemies within it

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

        lineRenderer = GetComponent<LineRenderer>();
        positions = new List<Vector3>();
    }

    public void Execute()
    {
        enabled = true;
        isActive = true;
        positions.Clear();
        timer = 0f;
        lineRenderer.positionCount = 0; // Reset the line renderer
    }

    public void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;

        if (timer > recordTime + cooldownTime)
        {
            isActive = false;
            lineRenderer.positionCount = 0;
            CheckForEnclosedEnemies();
            enabled = false;
            return;
        }

        if (timer <= recordTime)
        {
            // Record the player's position every frame
            positions.Add(transform.position);

            // Remove positions that are older than the record time
            while (positions.Count > 0 && timer > recordTime)
            {
                positions.RemoveAt(0);
                timer -= Time.deltaTime;
            }

            // Update the LineRenderer
            lineRenderer.positionCount = positions.Count;
            if (positions.Count > 1) // Ensure there are enough points to draw a line
            {
                lineRenderer.SetPositions(positions.ToArray());
            }
        }
    }

    private void CheckForEnclosedEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                if (IsPointEnclosed(enemy.transform.position))
                {
                    enemyHealth.TakeDamage(3.0f); // Assume there's a method to apply damage
                }
            }
        }
    }

    public bool IsPointEnclosed(Vector3 point)
    {
        // Ensure there are enough points to form a polygon
        if (positions.Count < 3) return false;

        int intersectionCount = 0;
        Vector3 lastPoint = positions[positions.Count - 1];

        foreach (Vector3 vertex in positions)
        {
            // Check if the ray intersects with the edge formed by the current and last vertex
            if ((vertex.y > point.y) != (lastPoint.y > point.y) &&
                point.x < (lastPoint.x - vertex.x) * (point.y - vertex.y) / (lastPoint.y - vertex.y) + vertex.x)
            {
                intersectionCount++;
            }
            lastPoint = vertex;
        }

        // If the intersection count is odd, the point is inside the polygon
        return intersectionCount % 2 == 1;
    }
}
