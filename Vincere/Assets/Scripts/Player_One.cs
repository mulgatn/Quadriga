using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_One : Car_Controller
{
    void Awake()
    { 
        playerNumber = 1;

        foreach (GameObject h in horses)
            h.SetActive(false);
        horses[PlayerPrefs.GetInt("Player1_Character")].SetActive(true);
    }
}
