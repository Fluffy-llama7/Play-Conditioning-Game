using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class TracePath : MonoBehaviour, IMechanic
{
    private Collider2D playerCollider;
    private GameObject enemy;
    private TrailRenderer trailRenderer;
    private List<Vector2> positionList;
    private bool active;
    [SerializeField]
    private float trackTime = 3.0f;
    private float elapsedTime;

    void Awake()
    {
        playerCollider = gameObject.GetComponent<Collider2D>();
        enemy = GameObject.Find("Enemy");
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
        }
    }

    /// <summary>
    /// Stops tracing the path
    /// </summary>
    public void Stop()
    {
        active = false;
        positionList.Clear();
        trailRenderer.enabled = false;
        trailRenderer.Clear();
    }
}
