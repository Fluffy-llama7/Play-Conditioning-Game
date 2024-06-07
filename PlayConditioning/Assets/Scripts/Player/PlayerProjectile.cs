using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 30.0f)
        {
            Destroy(this.gameObject);
            timer = 0.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
