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
    private Camera_Follow cam_follow;

    protected void Start()
    {
        lapCount = 0;
        isWon = false;
        isActive = true;
        movement = GetComponent<Player_Movement>();
        cam_follow = cam.GetComponent<Camera_Follow>();
    }

    protected void Update()
    {
        if (isActive)
        {
            if (movement.isControlled())
                cam_follow.rotationSpeed = 0.05f;
            else
                cam_follow.rotationSpeed = 0f;

            if (movement.breaking || movement.rotate != 0)
                cam_follow.rotationSpeed = 0.1f;
            else
                cam_follow.rotationSpeed = 0;
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
            GetComponent<Camera_Shake>().magnitude = movement.speed / 4f;
            GetComponent<Camera_Shake>().shakeTimer = GetComponent<Camera_Shake>().duration;
            movement.obstacleCollision();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bounds")
        {
            GetComponent<Camera_Shake>().magnitude = movement.speed / 2f;
            GetComponent<Camera_Shake>().shakeTimer = GetComponent<Camera_Shake>().duration;
        }       
    }
    protected void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bounds")
        {
            movement.BoundCollision();
        }   
        if(other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            movement.playerCollision();
        }
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        
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