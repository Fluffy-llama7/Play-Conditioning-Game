using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class TracePath : MonoBehaviour, IMechanic
{
    private Collider2D playerCollider;
    private GameObject enemy;
    private LineRenderer lineRenderer;
    private TrailRenderer trailRenderer;
    private List<Vector2> positionList;
    private bool active;
    [SerializeField]
    private float elapsedTime;
    private float radius = 3.0f;
    private float trackTime = 3.0f;
    private float thetaScale = 0.01f;

    void Awake()
    {
        playerCollider = gameObject.GetComponent<Collider2D>();
        
        enemy = GameObject.Find("Enemy");
        lineRenderer = enemy.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 110;
        lineRenderer.SetWidth(0.25f, 0.25f);

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = true;
        
        positionList = new List<Vector2>();
        active = false;
        elapsedTime = 0.0f;
    }

    /// <summary>
    /// Traces the path of the player
    /// </summary>
    public void Execute()
    {
        if (!active)
        {
            active = true;
        }
    }

    public void Update()
    {
        if (active)
        {
            if (elapsedTime >= trackTime)
            {
                trailRenderer.enabled = false;
            }
            else
            {
                elapsedTime += Time.deltaTime;
                positionList.Add(this.transform.position);
            }

            for (int i = 0; i < positionList.Count; i++) 
            {
                if (playerCollider.bounds.Contains(positionList[i]))
                {
                    Debug.Log("Collision detected between the player and the line.");
                }
            }

            for (int i = 0; i < positionList.Count - 1; i++) 
            {
                Collider2D hit1 = Physics2D.OverlapPoint(positionList[i], LayerMask.GetMask("Default"));
                Collider2D hit2 = Physics2D.OverlapPoint(positionList[i + 1], 
                    LayerMask.GetMask("Default"));

                if (hit1 != null && hit2 != null)
                {
                    Debug.Log("Collision detected within the line.");
                }
            }

            float theta = 0f;
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {          
                theta += (2.0f * Mathf.PI * thetaScale);         
                float x = radius * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(theta);
                x += enemy.transform.position.x;
                y += enemy.transform.position.y;
                lineRenderer.SetPosition(i, new Vector3(x, y, 1));
            }
        }
    }

    /// <summary>
    /// Stops tracing the path
    /// </summary>
    public void Stop()
    {
        active = false;
        positionList.Clear();
        lineRenderer.positionCount = 0;
        trailRenderer.enabled = false;
        trailRenderer.Clear();
    }
}
