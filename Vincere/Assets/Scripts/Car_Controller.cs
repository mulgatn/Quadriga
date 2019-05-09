using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent] Player_Movement
public class Car_Controller : MonoBehaviour
{
    public int playerNumber;
    protected bool isWon;
    private bool isActive;
    public bool boostReady;
    public int lapCount;
    protected int stereoPan;
    private Player_Movement movement;
    private GameObject shout_Icon;

    protected void Start()
    {
        isWon = false;
        isActive = true;
        movement = GetComponent<Player_Movement>();
        shout_Icon = transform.Find("Shout_Icon").gameObject;
        if(playerNumber == 1)
            FindObjectOfType<Audio_Manager>().Play("Player1_Sound", stereoPan);
    }
    protected void Update()
    {
         if (isActive)
         {
            if (boostReady)
                shout_Icon.SetActive(true);
            else
                shout_Icon.SetActive(false);
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