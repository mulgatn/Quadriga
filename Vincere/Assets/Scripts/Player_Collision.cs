﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Collision : MonoBehaviour
{
    private Rigidbody2D body;
    public GameObject[] check_Points;
    private Player_Movement movement;
    public CinemachineVirtualCamera cam;
    private Camera_Shake cam_shaker;
    public int checkPointCount;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<Player_Movement>();
        cam_shaker = GetComponent<Camera_Shake>();
        checkPointCount = 0;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (movement.speedMagnitude > 8f)
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
        if(other.gameObject == check_Points[checkPointCount])
        {
            checkPointCount++;
            if (checkPointCount == check_Points.Length)
            {
                checkPointCount = 0;
                GetComponent<Car_Controller>().lapCount++;
            }         
        }
    }
}