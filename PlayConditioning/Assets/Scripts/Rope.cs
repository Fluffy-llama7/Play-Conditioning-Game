using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer line;
    private GameObject ball;
    private Swing swing;

    void Start()
    {
        ball = GameObject.Find("Ball");
        swing = GameObject.Find("Player").GetComponent<Swing>();
        line = GetComponent<LineRenderer>();

        line.enabled = false;
    }

    void Update()
    {
        if (swing.IsActive())
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, ball.transform.position);
        }
        else
        {
            line.enabled = false;
        }
    }
}
