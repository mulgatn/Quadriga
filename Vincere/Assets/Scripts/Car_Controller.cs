using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    public int playerNumber;
    private int lapCount;
    protected bool isWon;
    private bool isActive;

    protected void Start()
    {
        lapCount = 0;
        isWon = false;
        isActive = true;
    }

    protected void Update()
    {
        if (isActive)
        {
            GetComponent<Player_Movement>().check();

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
            GetComponent<Player_Movement>().Movement();
    }
 
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Start_Line")
            lapCount++;
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Bounds")
        {
            GetComponent<Player_Movement>().BoundCollision();
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