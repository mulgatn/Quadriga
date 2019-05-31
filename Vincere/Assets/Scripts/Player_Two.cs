using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Two : Car_Controller
{
    void Awake()
    {
        playerNumber = 2;
        if (SceneManager.GetActiveScene().name == "Main_Menu")
            PlayerPrefs.SetInt("Player2_Character", 1);
        foreach (GameObject h in horses)
            h.SetActive(false);
        foreach (GameObject c in chariots)
            c.SetActive(false);
        horses[PlayerPrefs.GetInt("Player2_Character")].SetActive(true);
        chariots[PlayerPrefs.GetInt("Player2_Character")].SetActive(true);
        position = 0;
    }
}
