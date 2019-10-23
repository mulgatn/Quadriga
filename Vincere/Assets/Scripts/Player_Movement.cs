using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    private enum HORSECLOP
    {
        NONE,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE
    }
    private Rigidbody2D body;

    public float speedMagnitude;
    public float acceleration;
    public float torquePower;
    public float maxSpeed;
    public float minSpeed;

    public float breakPower;
    public bool breaking;

    public bool boostUsing;
    private bool boostUsed;
    public float speedBoost;
    public float speedBoostAcceleration;
    public float speedBoostDuration;

    public float rotate;
    public bool goingLeft;
    public bool goingRight;


    private KeyCode goLeft;
    private KeyCode goRight;
    private KeyCode goLeftAlt;
    private KeyCode goRightAlt;
    private string boost;

    private Car_Controller carController;
    private int playerNumber;

    public ParticleSystem dust;

    public AudioClip[] horseSounds;

    private HORSECLOP horseClop;

    private bool mainMenu;
    public float mainMenuBoostTimer;
    private float timer;


    [Range(.0f, 1f)]
    public float rotationMultiplier;


    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main_Menu")
            mainMenu = true;
        else
            mainMenu = false;
        horseClop = HORSECLOP.NONE;
        carController = GetComponent<Car_Controller>();
        body = GetComponent<Rigidbody2D>();
        if (carController.playerNumber == 1)
            playerNumber = 1;
        else if (carController.playerNumber == 2)
            playerNumber = 2;
        breaking = false;
        speedMagnitude += acceleration;
        if (carController.playerNumber == 1)
        {
            goRight = KeyCode.B;
            goLeft = KeyCode.V;
            goLeftAlt = KeyCode.Joystick1Button0;
            goRightAlt = KeyCode.Joystick1Button3;
            boost = "Player1_Boost";
        }
        else
        {
            goRight = KeyCode.Keypad2;
            goLeft = KeyCode.Keypad1;
            goLeftAlt = KeyCode.Joystick2Button0;
            goRightAlt = KeyCode.Joystick2Button3;
            boost = "Player2_Boost";
        }
    }

    public void check()
    {
        setRotation();

        speedMagnitude = body.velocity.magnitude;
        setHorseSpeed();

        var emission = dust.emission;
        emission.rateOverTime = speedMagnitude;

        if (isControlled())
            body.freezeRotation = false;
        else
            body.freezeRotation = true;

        
        if (Input.GetKey(goLeft) || Input.GetKey(goLeftAlt))
            goingLeft = true;
            
        else
            goingLeft = false;
        if (Input.GetKey(goRight) || Input.GetKey(goRightAlt))
            goingRight = true;
        else
            goingRight = false;
        if((Input.GetKey(goRight) || Input.GetKey(goRightAlt)) && (Input.GetKey(goLeft) || Input.GetKey(goLeftAlt)))
        {
            goingLeft = false;
            goingRight = false;
        }

        if (rotate != 0f)
            if (speedMagnitude == minSpeed)
                speedMagnitude += acceleration;

        breakCheck(playerNumber);

        if (Input.GetButtonDown(boost) && carController.boostReady)
        {
            if(FindObjectOfType<Audio_Manager>())
            {
                    if (!FindObjectOfType<Audio_Manager>().isPlaying("Speed_Boost"))
                        FindObjectOfType<Audio_Manager>().Play("Speed_Boost");
            }
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

        if (mainMenu)
            mainMenuSpeedBoost();
    }

    public void Movement()
    {
        if (!breaking)
            body.angularVelocity = (rotate * torquePower);
        if (speedMagnitude < maxSpeed && speedMagnitude != minSpeed)
        {
            body.AddForce(transform.up * acceleration * maxSpeed);
        }
        body.velocity = ForwardVelocity();
        if (goingRight || goingLeft)
        {
            if (goingRight)
            {
                Vector2 temp = transform.right * body.velocity.magnitude / 3.5f;
                body.velocity = body.velocity + temp;
            }
            else
            {
                Vector2 temp = -transform.right * body.velocity.magnitude / 3.5f;
                body.velocity = body.velocity + temp;
            }
        }

        if (boostUsing && !breaking)
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
        //body.velocity = Vector2.zero;
        body.angularVelocity = 0;
        maxSpeed /= 2;
    }

    public void QuitCollision()
    {
            maxSpeed = 15f;
    }

    public void obstacleCollision(int obstacleID)
    {
        if (obstacleID == 0)
            body.velocity /= 2f;
        else if (obstacleID == 1)
            body.velocity /= 1.5f;
        body.angularVelocity = 0;
    }

    public void playerCollision()
    {
        body.velocity /= 2f;
        body.angularVelocity = 0;
        maxSpeed /= 2;
        if (FindObjectOfType<Audio_Manager>() && speedMagnitude > 7.5f)
            FindObjectOfType<Audio_Manager>().Play("Player_Player_Collision");
    }

    public bool isControlled()
    {
        if (Input.GetButton("Player1_Rotation") || Input.GetButton("Player2_Rotation"))
            return true;
        else
            return false;
    }

    private void breakCheck(int p_playerNumber)
    {
        bool isPressed = false;
        if (p_playerNumber == 1)
            isPressed = Input.GetButton("Player1_Rotation");
        else
            isPressed = Input.GetButton("Player2_Rotation");

        if (isPressed && rotate == 0)
        {
            speedMagnitude -= breakPower;
            breaking = true;
            if (speedMagnitude < minSpeed)
                 speedMagnitude = minSpeed;
            body.angularVelocity /= 1.1f;
        }
        else
            breaking = false;
    }

    private void setHorseSpeed()
    {
        if(FindObjectOfType<Audio_Manager>())
        {
            if(playerNumber == 1)
            {
                if(body.velocity.magnitude < 0.5f)
                {
                    FindObjectOfType<Audio_Manager>().Stop("Player1_Horse");
                    horseClop = HORSECLOP.NONE;
                }
                   
                else if (body.velocity.magnitude > 0.5f && body.velocity.magnitude < 3f)
                {
                    if(horseClop != HORSECLOP.ONE)
                        FindObjectOfType<Audio_Manager>().Stop("Player1_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player1_Horse", horseSounds[0]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player1_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player1_Horse");
                    horseClop = HORSECLOP.ONE;
                }
                else if (body.velocity.magnitude > 3f && body.velocity.magnitude < 6f)
                {
                    if (horseClop != HORSECLOP.TWO)
                        FindObjectOfType<Audio_Manager>().Stop("Player1_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player1_Horse", horseSounds[1]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player1_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player1_Horse");
                    horseClop = HORSECLOP.TWO;
                }
                else if (body.velocity.magnitude > 6f && body.velocity.magnitude < 9f)
                {
                    if (horseClop != HORSECLOP.THREE)
                        FindObjectOfType<Audio_Manager>().Stop("Player1_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player1_Horse", horseSounds[2]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player1_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player1_Horse");
                    horseClop = HORSECLOP.THREE;
                }
                else if (body.velocity.magnitude > 9f && body.velocity.magnitude < 12f)
                {
                    if (horseClop != HORSECLOP.FOUR)
                        FindObjectOfType<Audio_Manager>().Stop("Player1_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player1_Horse", horseSounds[3]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player1_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player1_Horse");
                    horseClop = HORSECLOP.FOUR;
                }
                else if (body.velocity.magnitude > 12f)
                {
                    if (horseClop != HORSECLOP.FIVE)
                        FindObjectOfType<Audio_Manager>().Stop("Player1_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player1_Horse", horseSounds[4]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player1_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player1_Horse");
                    horseClop = HORSECLOP.FIVE;
                }
            }
            else
            {
                if (body.velocity.magnitude < 0.5f)
                {
                    FindObjectOfType<Audio_Manager>().Stop("Player2_Horse");
                    horseClop = HORSECLOP.NONE;
                }

                else if (body.velocity.magnitude > 0.5f && body.velocity.magnitude < 3f)
                {
                    if (horseClop != HORSECLOP.ONE)
                        FindObjectOfType<Audio_Manager>().Stop("Player2_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player2_Horse", horseSounds[0]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player2_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player2_Horse");
                    horseClop = HORSECLOP.ONE;
                }
                else if (body.velocity.magnitude > 3f && body.velocity.magnitude < 6f)
                {
                    if (horseClop != HORSECLOP.TWO)
                        FindObjectOfType<Audio_Manager>().Stop("Player2_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player2_Horse", horseSounds[1]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player2_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player2_Horse");
                    horseClop = HORSECLOP.TWO;
                }
                else if (body.velocity.magnitude > 6f && body.velocity.magnitude < 9f)
                {
                    if (horseClop != HORSECLOP.THREE)
                        FindObjectOfType<Audio_Manager>().Stop("Player2_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player2_Horse", horseSounds[2]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player2_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player2_Horse");
                    horseClop = HORSECLOP.THREE;
                }
                else if (body.velocity.magnitude > 9f && body.velocity.magnitude < 12f)
                {
                    if (horseClop != HORSECLOP.FOUR)
                        FindObjectOfType<Audio_Manager>().Stop("Player2_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player2_Horse", horseSounds[3]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player2_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player2_Horse");
                    horseClop = HORSECLOP.FOUR;
                }
                else if (body.velocity.magnitude > 12f)
                {
                    if (horseClop != HORSECLOP.FIVE)
                        FindObjectOfType<Audio_Manager>().Stop("Player2_Horse");
                    FindObjectOfType<Audio_Manager>().setClip("Player2_Horse", horseSounds[4]);
                    if (!FindObjectOfType<Audio_Manager>().isPlaying(("Player2_Horse")))
                        FindObjectOfType<Audio_Manager>().Play("Player2_Horse");
                    horseClop = HORSECLOP.FIVE;
                }
            }
        }
    }

    private void mainMenuSpeedBoost()
    {
        timer += Time.deltaTime;
        if (timer > mainMenuBoostTimer)
        {
            carController.boostReady = true;
            timer = 0f;
        }         
    }

    private void setRotation()
    {
        if (playerNumber == 1)
        {
            if (Input.GetButton("Player1_Rotation"))
                rotate += Input.GetAxisRaw("Player1_Rotation") * rotationMultiplier;
            else
                rotate = 0;
        }
        else if (playerNumber == 2)
        {
            if (Input.GetButton("Player2_Rotation"))
                rotate += Input.GetAxisRaw("Player2_Rotation") * rotationMultiplier;
            else
                rotate = 0;
        }

        if (rotate > 1f)
            rotate = 1f;
        if (rotate < -1f)
            rotate = -1f;
    }
}