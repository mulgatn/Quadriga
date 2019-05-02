using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent] Player_Movement
public class Car_Controller : MonoBehaviour
{
    public int playerNumber;
    protected bool isWon;
    private bool isActive;
    public int lapCount;
    private Player_Movement movement;

    protected void Start()
    {
        isWon = false;
        isActive = true;
        movement = GetComponent<Player_Movement>();

    }
    protected void Update()
    {
         if (isActive)
         {
             movement.check();
            if (lapCount == 3)
            {
                PlayerPrefs.SetInt("Winner", playerNumber);
                isWon = true;
            }
        }
    }
    protected void FixedUpdate()
    {
        if (isActive)
            movement.Movement();
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