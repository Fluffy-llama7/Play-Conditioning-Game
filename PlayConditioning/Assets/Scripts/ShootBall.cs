using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;

public class ShootBall : ScriptableObject
{
    private GameObject ball;

    void Awake()
    {
        ball = GameObject.Find("Ball");
    }

    public void Execute(GameObject player)
    {

    }

    public void Stop()
    {

    }
}
