using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Collision : MonoBehaviour
{
    public GameObject[] check_Points;
    private Player_Movement movement;
    private Car_Controller carController;
    public Car_Controller otherPlayer;
    public CinemachineVirtualCamera cam;
    private Camera_Shake cam_shaker;
    public int checkPointCount;
    private int obstacleID;

    public Lap_Counter lapHandler;
    void Start()
    {
        movement = transform.parent.gameObject.GetComponent<Player_Movement>();
        cam_shaker = transform.parent.gameObject.GetComponent<Camera_Shake>();
        carController = transform.parent.gameObject.GetComponent<Car_Controller>();
        checkPointCount = 0;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {    
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
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            if (cam_shaker.shakeTimer == 0f)
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

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            obstacleID = other.gameObject.GetComponent<Animator>().GetInteger("Obstacle_Identifier");
            if (carController.playerNumber == 1)
            {
                if (FindObjectOfType<Audio_Manager>())
                {
                    if(obstacleID == 0)
                        FindObjectOfType<Audio_Manager>().Play("Crate_Break_Player1");
                    else
                        FindObjectOfType<Audio_Manager>().Play("Cushion_Rip_Player1");
                }
                    
            }
            else
            {
                if (FindObjectOfType<Audio_Manager>())
                {
                    if (obstacleID == 0)
                        FindObjectOfType<Audio_Manager>().Play("Crate_Break_Player2");
                    else
                        FindObjectOfType<Audio_Manager>().Play("Cushion_Rip_Player2");
                }
            }
            if (!movement.boostUsing)
            {
                if (movement.speedMagnitude > 8f)
                {
                    cam_shaker.magnitude = movement.speedMagnitude / 2f;
                    cam_shaker.shakeTimer = cam_shaker.duration;
                }

                movement.obstacleCollision(obstacleID);
            }

            other.gameObject.GetComponent<Animator>().SetBool("Broken", true);
        }
        if (other.gameObject == check_Points[checkPointCount])
        {
            checkPointCount++;
            if (checkPointCount == check_Points.Length)
            {
                checkPointCount = 0;
                if (otherPlayer.lapCount > carController.lapCount)
                    carController.boostReady = true;
                else
                    carController.boostReady = false;
                carController.lapCount++;
                lapHandler.nextLap();
            }         
        }
        
    }
}
