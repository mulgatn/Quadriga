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
    private int lapCheckPointCount;
    public int totalCheckPointCount;
    private int obstacleID;
    private int playerNumber;

    public Lap_Counter lapHandler;
    void Start()
    {
        movement = transform.parent.gameObject.GetComponent<Player_Movement>();
        cam_shaker = transform.parent.gameObject.GetComponent<Camera_Shake>();
        carController = transform.parent.gameObject.GetComponent<Car_Controller>();
        lapCheckPointCount = 0;
        totalCheckPointCount = 0;
        playerNumber = carController.playerNumber;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {    
        if (other.gameObject.tag == "Bounds")
        {
            if (movement.speedMagnitude > 7f)
            {
                playWallCollision();
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
                    FindObjectOfType<Audio_Manager>().Play("Obstacle_Hit");
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
                    FindObjectOfType<Audio_Manager>().Play("Obstacle_Hit");
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
        if (other.gameObject == check_Points[lapCheckPointCount])
        {
            lapCheckPointCount++;
            totalCheckPointCount++;
            if (totalCheckPointCount > otherPlayer.transform.Find("Horses").GetComponent<Player_Collision>().totalCheckPointCount)
            {
                carController.position = 0;
                otherPlayer.position = 1;
            }
                

            if (lapCheckPointCount == check_Points.Length)
            {
                    lapCheckPointCount = 0;
                if (otherPlayer.lapCount > carController.lapCount)
                    carController.boostReady = true;
                else
                    carController.boostReady = false;
                carController.lapCount++;
                lapHandler.nextLap();
            }         
        }
        
    }

    private void playWallCollision()
    {
        if (FindObjectOfType<Audio_Manager>())
        {
            if (playerNumber == 1)
            {
                if (!FindObjectOfType<Audio_Manager>().isPlaying("Player1_Wall"))
                    FindObjectOfType<Audio_Manager>().Play("Player1_Wall");
            }
            else if (playerNumber == 2)
            {
                if (!FindObjectOfType<Audio_Manager>().isPlaying("Player2_Wall"))
                    FindObjectOfType<Audio_Manager>().Play("Player2_Wall");
            }
        }
    }
}
