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
    public bool slowedDown;
    public bool speededUp;
    Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        breaking = false;
    }

    private void Update()
    {     
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {        
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
        if (speed == minSpeed)
            slowedDown = true;
        else
            slowedDown = false;
        if (speed == maxSpeed)
            speededUp = true;
        else
            speededUp = false;


    }
    void FixedUpdate()
    {   
        body.velocity = transform.up * speed;
        body.angularVelocity = (Input.GetAxis("Horizontal") * torquePower);
    }
}