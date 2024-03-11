using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mech;

public class ChargeBlast : MonoBehaviour, IMechanic
{
    private GameObject blast;
    private TrailRenderer trailRenderer;
    private bool active;
    [SerializeField] 
    private float blastSpeed = 2.0f;
    [SerializeField] 
    private float chargeFactor = 0.5f;

    void Awake()
    {
        blast = GameObject.Find("Blast");
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        active = false;
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
