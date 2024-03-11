using Mech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;
using UnityEditor;

public class Shooting : MonoBehaviour, IMechanic
{
    public GameObject player;
    public GameObject bullet;
    Vector2 mousepos;
    public Transform gun;
    float angle;
    public float speed = 50.0f;
    GameObject bulletClone;
    // Start is called before the first frame update
    public void Execute()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousepos.y, mousepos.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0, 0, angle);
        bulletClone = Instantiate(bullet);

    }

    // Update is called once per frame
    public void Update()
    {
    //if statement for testing mechanic 
        if (Input.GetMouseButtonDown(0))
        {
            Execute();
        }
            

        bulletClone.GetComponent<Rigidbody2D>().velocity = gun.right * speed;
    }
}
