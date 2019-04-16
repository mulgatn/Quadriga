using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float torquePower;
    public float maxSpeed;
    public float minSpeed;
    public float breakPower;
    public bool breaking;
    float rotate;
    Rigidbody2D body;
    KeyCode turnLeft;
    KeyCode turnRight;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        breaking = false;
        turnLeft = KeyCode.A;
        turnRight = KeyCode.D;
    }

    private void Update()
    {
        rotate = Input.GetAxis("Player1_Rotation");
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
        print(rotate);
    }
    void FixedUpdate()
    {   
        body.velocity = transform.up * speed;
        if(!breaking)
            body.angularVelocity = (rotate * torquePower);
    }
}