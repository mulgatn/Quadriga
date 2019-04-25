using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent] Player_Movement
public class Car_Controller : MonoBehaviour
{
    public int playerNumber;
    private int lapCount;
    protected bool isWon;
    private bool isActive;
    private Player_Movement movement;

    protected void Start()
    {
        lapCount = 0;
        isWon = false;
        isActive = true;
        movement = GetComponent<Player_Movement>();
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
            movement.resetSpeed();
            Destroy(other.gameObject);
        }
    }
    protected void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bounds")
        {
            //GetComponent<Camera_Shake>().shakeReady = true;
            movement.BoundCollision();
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