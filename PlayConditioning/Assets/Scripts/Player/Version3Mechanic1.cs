using Mech;
using System.Collections.Generic;
using UnityEngine;

public class Version3Mechanic1 : MonoBehaviour, IMechanic
{
    public float recordTime = 5.0f; // Time in seconds to keep track of the player's position
    private LineRenderer lineRenderer;
    private List<Vector3> positions;
    private float timer;
    private bool isActive = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        positions = new List<Vector3>();
        enabled = false;
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

        // Check if any enemies are enclosed by the path
        CheckForEnclosedEnemies();
    }

    private void CheckForEnclosedEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.isEnclosed = IsPointEnclosed(enemy.transform.position);
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

    // Point-in-polygon algorithm to determine if a point is inside a polygon
    private bool IsPointInPolygon(List<Vector3> polygon, Vector3 point)
    {
        int polygonLength = polygon.Count, i = 0;
        bool inside = false;

        // Get the point in 2D space
        float pointX = point.x, pointY = point.y;
        float startX, startY, endX, endY;
        Vector3 endPoint = polygon[polygonLength - 1];
        endX = endPoint.x;
        endY = endPoint.y;
        while (i < polygonLength)
        {
            startX = endX;
            startY = endY;
            endPoint = polygon[i++];
            endX = endPoint.x;
            endY = endPoint.y;
            inside ^= (endY > pointY ^ startY > pointY) && (pointX < (startX - endX) * (pointY - endY) / (startY - endY) + endX);
        }
        return inside;
    }
}
