using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class TracePath : MonoBehaviour, IMechanic
{
    private Collider2D playerCollider;
    private LineRenderer lineRenderer;
    private List<Vector2> positionList;
    private bool active;
    // TODO finalize this value
    private float trackTime = 3.0f;
    private float elapsedTime;

    void Awake()
    {
        gameObject.transform.hasChanged = false;
        playerCollider = gameObject.GetComponent<Collider2D>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        positionList = new List<Vector2>();
        active = false;
        elapsedTime = 0.0f;
    }

    /// <summary>
    /// Traces the path of the player
    /// </summary>
    public void Execute()
    {
        if(!active)
        {
            active = true;
            elapsedTime = 0.0f;
        }
    }

    public void Update()
    {
        if (active)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime <= trackTime)
            {
                if (gameObject.transform.hasChanged)
                {
                    positionList.Add(gameObject.transform.position);
                    gameObject.transform.hasChanged = false;
                }
            }

            lineRenderer.enabled = true;
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = positionList.Count;
            for (int i = 0; i < positionList.Count; i++)
            {
                lineRenderer.SetPosition(i, positionList[i]);
            }

            for (int i = 0; i < lineRenderer.positionCount; i++) 
            {
                if (playerCollider.bounds.Contains(lineRenderer.GetPosition(i)))
                {
                    Debug.Log("Collision detected between the player and the line.");
                }
            }
        }
    }

    /// <summary>
    /// Stops tracing the path
    /// </summary>
    public void Stop()
    {
        gameObject.transform.hasChanged = false;
        active = false;
        positionList.Clear();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 0;
    }
}
