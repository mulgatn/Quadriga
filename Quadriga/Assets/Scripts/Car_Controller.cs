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
    private int lapCount;
    protected bool isWon;
    private bool isActive;

    protected void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        breaking = false;
        lapCount = 0;
        isWon = false;
        isActive = true;
    }

    protected void Update()
    {
        if (isActive)
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
            if (lapCount == 2)
            {
                PlayerPrefs.SetInt("Winner", playerNumber);
                isWon = true;
            }
        }     
    }
    protected void FixedUpdate()
    {   
        if(isActive)
        {
            body.velocity = transform.up * speed;
            if (!breaking)
                body.angularVelocity = (rotate * torquePower);
        }  
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Start_Line")
            lapCount++;
    }

    public bool checkWin()
    {
        return isWon;
    }

    public void setActivity(bool p_isActive)
    {
        isActive = p_isActive;
    }
    
    public bool getActivity()
    {
        return isActive;
    }
}