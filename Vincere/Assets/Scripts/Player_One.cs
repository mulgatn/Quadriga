using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_One : Car_Controller
{
    void Awake()
    { 
        playerNumber = 1;
        if (SceneManager.GetActiveScene().name == "Main_Menu")
            PlayerPrefs.SetInt("Player1_Character", 0);
        foreach (GameObject h in horses)
            h.SetActive(false);
        foreach (GameObject c in chariots)
            c.SetActive(false);
        horses[PlayerPrefs.GetInt("Player1_Character")].SetActive(true);
        chariots[PlayerPrefs.GetInt("Player1_Character")].SetActive(true);
        position = 1;
    }
}
