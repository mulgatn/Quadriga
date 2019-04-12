using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float torquePower;
    public float maxSpeed;
    public float breakPower;
    public bool breaking;
    Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        breaking = false;
    }

    private void Update()
    {
        Debug.Log(breaking);
        if (speed < maxSpeed )
            speed += acceleration;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && speed > 0.02f)
        {
            speed -= breakPower;
            breaking = true;
        }
        else
            breaking = false;
            
    }
    void FixedUpdate()
    {   
        body.velocity = transform.up * speed;
        body.angularVelocity = (Input.GetAxis("Horizontal") * torquePower);
    }
}