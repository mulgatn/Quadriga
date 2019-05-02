using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//[RequireComponent] Player_Movement
public class Car_Controller : MonoBehaviour
{
    public int playerNumber;
    private int lapCount;
    protected bool isWon;
    private bool isActive;
    private Player_Movement movement;
    public CinemachineVirtualCamera cam;
    private Camera_Shake cam_shaker;

    protected void Start()
    {
        lapCount = 0;
        isWon = false;
        isActive = true;
        movement = GetComponent<Player_Movement>();
        cam_shaker = GetComponent<Camera_Shake>();
    }

    protected void Update()
    {
        if (isActive)
        {
            movement.check();

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
            movement.Movement();
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Start_Line")
            lapCount++;

    }
    protected void OnCollisionEnter2D(Collision2D other)
    {     
        if (other.gameObject.tag == "Obstacle")
        {
            if(movement.speedMagnitude > 8f)
            {
                cam_shaker.magnitude = movement.speedMagnitude / 2f;
                cam_shaker.shakeTimer = cam_shaker.duration;
            }        
            movement.obstacleCollision();
            other.gameObject.GetComponent<Animator>().SetBool("Broken", true);
        }
        if (other.gameObject.tag == "Bounds")
        {
            if (movement.speedMagnitude > 7f)
            {
                cam_shaker.magnitude = movement.speedMagnitude;
                cam_shaker.shakeTimer = cam_shaker.duration;
            }
            movement.BoundCollision();
        }

        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            cam_shaker.magnitude = movement.speedMagnitude / 3f;
            cam_shaker.shakeTimer = cam_shaker.duration;
            movement.playerCollision();
        }
    }
    protected void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bounds")
        {
        }   
        if(other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            if(cam_shaker.shakeTimer == 0f)
            {
                cam_shaker.magnitude = movement.speedMagnitude / 10f;
                cam_shaker.shakeTimer = cam_shaker.duration;
            }    
        }
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bounds" || other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            movement.QuitCollision();
        }
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