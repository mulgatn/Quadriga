using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    protected float speed;
    public float acceleration;
    public float torquePower;
    public float maxSpeed;
    public float minSpeed;
    public float breakPower;
    protected bool breaking;
    protected float rotate;
    protected Rigidbody2D body;
    protected KeyCode turnLeft;
    protected KeyCode turnRight;
    protected int playerNumber;

    protected void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        breaking = false;
    }

    protected void Update()
    {
        if (playerNumber == 1)
            rotate = Input.GetAxis("Player1_Rotation");
        else if (playerNumber == 2)
            rotate = Input.GetAxis("Player2_Rotation");
        if (Input.GetKey(turnLeft) && Input.GetKey(turnRight))
        {
            if (rotate > 0)
                rotate = 1;
            if (rotate < 0)
                rotate = -1;
            speed -= breakPower;
            breaking = true;
            if (speed < minSpeed)
                speed = minSpeed;
        }
        else
        {
            if (speed < maxSpeed)
                speed += acceleration;
            else speed = maxSpeed;
            breaking = false;
        }
    }
    protected void FixedUpdate()
    {   
        body.velocity = transform.up * speed;
        if(!breaking)
            body.angularVelocity = (rotate * torquePower);
    }
}