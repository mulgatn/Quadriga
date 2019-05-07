using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D body;

    public float speedMagnitude;
    public float acceleration;
    public float torquePower;
    public float maxSpeed;
    public float minSpeed;

    public float breakPower;
    public bool breaking;

    private bool boostUsing;
    private bool boostUsed;
    public float speedBoost;
    public float speedBoostAcceleration;
    public float speedBoostDuration;

    public float rotate;
    public bool goingLeft;
    public bool goingRight;


    private KeyCode turnLeft;
    private KeyCode turnRight;
    private KeyCode goLeft;
    private KeyCode goRight;
    private KeyCode turnLeftAlt;
    private KeyCode turnRightAlt;
    private KeyCode goLeftAlt;
    private KeyCode goRightAlt;
    private string boost;

    private Car_Controller carController;


    void Start()
    {
        carController = GetComponent<Car_Controller>();
        body = GetComponent<Rigidbody2D>();
        breaking = false;
        speedMagnitude += acceleration;
        if (carController.playerNumber == 1)
        {
            turnLeft = KeyCode.A;
            turnRight = KeyCode.D;
            goRight = KeyCode.B;
            goLeft = KeyCode.V;
            boost = "Player1_Boost";
        }
        else
        {
            turnLeft = KeyCode.LeftArrow;
            turnRight = KeyCode.RightArrow;
            goRight = KeyCode.Keypad2;
            goLeft = KeyCode.Keypad1;
            turnLeftAlt = KeyCode.JoystickButton4;
            turnRightAlt = KeyCode.JoystickButton5;
            goLeftAlt = KeyCode.JoystickButton6;
            goRightAlt = KeyCode.JoystickButton7;
            boost = "Player2_Boost";
        }
    }

    public void check()
    {
        if (carController.playerNumber == 1)
            rotate = Input.GetAxisRaw("Player1_Rotation");
        else if (carController.playerNumber == 2)
            rotate = Input.GetAxisRaw("Player2_Rotation");

        speedMagnitude = body.velocity.magnitude;

        if (isControlled())
            body.freezeRotation = false;
        else
            body.freezeRotation = true;

        if (rotate != 0f)
            if (speedMagnitude == minSpeed)
                speedMagnitude += acceleration;
        if (Input.GetKey(goLeft) || Input.GetKey(goLeftAlt))
            goingLeft = true;
        else
            goingLeft = false;
        if (Input.GetKey(goRight) || Input.GetKey(goRightAlt))
            goingRight = true;
        else
            goingRight = false;

        if ((Input.GetKey(turnLeft) || Input.GetKey(turnLeftAlt)) && (Input.GetKey(turnRight) || Input.GetKey(turnRightAlt)))
        {
            speedMagnitude -= breakPower;
            breaking = true;
            if (speedMagnitude < minSpeed)
                speedMagnitude = minSpeed;
        }
        else
            breaking = false;

        if (Input.GetButtonDown(boost) && carController.boostReady)
        {
            boostUsing = true;
            carController.boostReady = false;
            boostUsed = true;
        }

        if (boostUsed)
        {
            Invoke("resetBoost", speedBoostDuration);
            boostUsed = false;
        }

        if(maxSpeed < 8f)
        {
            goingLeft = false;
            goingRight = false;
        }
    }

    public void Movement()
    {
        if (!breaking)
            body.angularVelocity = (rotate * torquePower);
        if (speedMagnitude < maxSpeed && speedMagnitude != minSpeed)
            body.AddForce(transform.up * acceleration * maxSpeed);
        body.velocity = ForwardVelocity();
        if (goingRight || goingLeft)
        {
            if (goingRight)
            {
                Vector2 temp = transform.right * 3f;
                body.velocity = body.velocity + temp;
            }
            else
            {
                Vector2 temp = -transform.right * 3f;
                body.velocity = body.velocity + temp;
            }
        }
        if (boostUsing)
            speedUp();
    }

    private Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(body.velocity, transform.up);
    }

    public void speedUp()
    {
        body.AddForce(transform.up * speedBoost * speedBoostAcceleration);
    }

    private void resetBoost()
    {
        boostUsed = false;
        boostUsing = false;
    }

    public void BoundCollision()
    {
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
        maxSpeed /= 2;
    }

    public void QuitCollision()
    {
            maxSpeed = 15f;
    }

    public void obstacleCollision()
    {
        body.velocity /= 2f;
        body.angularVelocity = 0;
    }

    public void playerCollision()
    {
        body.velocity /= 2f;
        body.angularVelocity = 0;
        maxSpeed /= 2;
    }

    public bool isControlled()
    {
        if (Input.GetButton("Player1_Rotation") || Input.GetButton("Player2_Rotation"))
            return true;
        else
            return false;
    }
}