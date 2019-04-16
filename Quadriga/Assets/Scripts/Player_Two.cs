using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Two : Car_Controller
{
    void Start()
    {
        turnLeft = KeyCode.LeftArrow;
        turnRight = KeyCode.RightArrow;
        playerNumber = 2;
    }
}
