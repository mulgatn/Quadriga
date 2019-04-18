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
    protected KeyCode goLeft;
    protected KeyCode goRight;
    protected int playerNumber;
    private int lapCount;
    protected bool isWon;
    private bool isActive;
    private bool goingLeft;
    private bool goingRight;

    protected void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        breaking = false;
        lapCount = 0;
        isWon = false;
        isActive = true;
        speed += acceleration;
    }

    protected void Update()
    {
        if (isActive)
        {
            if (playerNumber == 1)
                rotate = Input.GetAxis("Player1_Rotation");
            else if (playerNumber == 2)
                rotate = Input.GetAxis("Player2_Rotation");
            if(rotate != 0f)
                if (speed == minSpeed)
                    speed += acceleration;
            if (Input.GetKey(goLeft))
                goingLeft = true;
            else
                goingLeft = false;
            if (Input.GetKey(goRight))
                goingRight = true;
            else
                goingRight = false;
                
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
                if (speed < maxSpeed && speed != minSpeed)
                    speed += acceleration;
                else if(speed > maxSpeed)
                    speed = maxSpeed;
                breaking = false;
            }


            if (lapCount == 4)
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
            if (!breaking)
                body.angularVelocity = (rotate * torquePower);
            if (body.velocity.magnitude < maxSpeed && speed != minSpeed)
                body.AddForce(transform.up * acceleration * 10f);

            if (goingRight || goingLeft)
            {
                if (goingRight)
                {
                    Vector2 temp = transform.right * 0.2f;
                    body.velocity = body.velocity + temp;
                }
                else
                {
                    Vector2 temp = -transform.right * 0.2f;
                    body.velocity = body.velocity + temp;
                }
            }
            else
                body.velocity = ForwardVelocity();
        }  
    }
 
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Start_Line")
            lapCount++;
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        speed = minSpeed;
        body.velocity = Vector2.zero;
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

    private Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(body.velocity, transform.up);
    }
}