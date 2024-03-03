using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mech;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    public float speed = 10.0f;
    private IMechanic left1;
    private IMechanic right1;
    private IMechanic left2;
    private IMechanic right2;
    private IMechanic left3;
    private IMechanic right3;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public void Damage(GameObject enemy)
    {

    }

    public void OnLeftClick(GameObject player, string mechanic)
    {

    }

    public void OnRightClick(GameObject player, string mechanic)
    {

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}