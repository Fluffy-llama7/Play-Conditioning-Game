using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class Blast : MonoBehaviour, IMechanic
{
    private GameObject blast;
    private TrailRenderer trailRenderer;
    private Vector2 targetPosition;
    private bool active;
    private bool gracePeriodFlag;
    private float elapsedTime;
    [SerializeField] 
    private float blastSpeed = 2.0f;
    [SerializeField] 
    private float blastStrength = 4.0f;
    [SerializeField] 
    private float blastGracePeriod = 3.0f;
    [SerializeField] 
    private float moveWaitTime = 2.0f;

    void Awake()
    {
        blast = GameObject.Find("Blast");
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        active = false;
        gracePeriodFlag = false;
        elapsedTime = 0.0f;
    }

    /// <summary>
    /// Charges up the blast
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
            if (!gracePeriodFlag)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= moveWaitTime)
                {
                    targetPosition = this.transform.position;
                    elapsedTime = 0.0f;
                    gracePeriodFlag = true;
                    Debug.Log("The grace period has started.");
                }
            }
            else
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= blastGracePeriod)
                {
                    blast.transform.position = Vector2.MoveTowards(blast.transform.position, targetPosition, 
                        blastSpeed * Time.deltaTime);
                    if (Vector2.Distance(blast.transform.position, targetPosition) <= 0)
                    {
                        elapsedTime = 0.0f;
                        gracePeriodFlag = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Stops charging up the blast
    /// </summary>
    public void Stop()
    {
        active = false;
    }
}
