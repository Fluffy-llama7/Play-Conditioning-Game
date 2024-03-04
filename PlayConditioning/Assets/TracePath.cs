using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class TracePath : MonoBehaviour, IMechanic
{
    private LineRenderer lineRenderer;
    private List<Vector3> positionList;
    private bool active;
    // TODO finalize this value
    private float trackTime = 2.0f;
    private float elapsedTime;

    void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        positionList = new List<Vector3>();
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
                positionList.Add(gameObject.transform.position);
            }

            lineRenderer.enabled = true;
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = positionList.Count;
            for (int i = 0; i < positionList.Count; i++)
            {
                lineRenderer.SetPosition(i, positionList[i]);
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
        lineRenderer.enabled = false;
    }
}
