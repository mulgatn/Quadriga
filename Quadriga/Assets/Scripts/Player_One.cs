using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_One : Car_Controller
{
    void Start()
    { 
        turnLeft = KeyCode.A;
        turnRight = KeyCode.D;
        playerNumber = 1;   
    }
}
