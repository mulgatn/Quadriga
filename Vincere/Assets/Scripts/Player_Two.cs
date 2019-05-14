using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Two : Car_Controller
{
    void Awake()
    {
        playerNumber = 2;
        PlayerPrefs.SetInt("Player2_Character", 1);
        foreach (GameObject h in horses)
            h.SetActive(false);
        horses[PlayerPrefs.GetInt("Player2_Character")].SetActive(true);
    }
}
