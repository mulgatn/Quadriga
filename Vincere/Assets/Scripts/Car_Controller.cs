using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent] Player_Movement
public class Car_Controller : MonoBehaviour
{
    public int playerNumber;
    protected bool isWon;
    private bool isActive;
    public bool boostReady;
    public int lapCount;
    [HideInInspector]
    public int position;


    private Player_Movement movement;
    private GameObject shout_Icon;
    public GameObject[] horses;
    public GameObject[] chariots;
    public Image positionID;
    public Sprite[] posSprites;


    protected void Start()
    {
        isWon = false;
        isActive = true;
        movement = GetComponent<Player_Movement>();
        shout_Icon = transform.Find("Shout_Icon").gameObject;
    }
    protected void Update()
    {
         if (isActive)
         {
            positionID.sprite = posSprites[position];
            if (boostReady)
                shout_Icon.SetActive(true);
            else
                shout_Icon.SetActive(false);
            movement.check();
            if (lapCount == 7)
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