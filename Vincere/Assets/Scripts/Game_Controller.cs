﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    private GameObject playerOne;
    private GameObject playerTwo;
    public Car_Controller[] playerScripts;
    bool gameOver;

    private void Start()
    {
        gameOver = false;
        playerScripts = new Car_Controller[2];
        playerOne = GameObject.FindGameObjectWithTag("Player1");
        playerScripts[0] = playerOne.GetComponent<Player_One>();
        playerTwo = GameObject.FindGameObjectWithTag("Player2");
        playerScripts[1] = playerTwo.GetComponent<Player_Two>();

        FindObjectOfType<Audio_Manager>().Play("Crowd_In_Game", 0f);
    }

    private void Update()
    {
        foreach(Car_Controller playerScript in playerScripts)
        {
            if (playerScript.checkWin())
            {
                playerScripts[0].setActivity(false);
                playerScripts[1].setActivity(false);
                gameOver = true;
            }
        }
        if (gameOver)
            SceneManager.LoadScene("End_Screen");
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("Level_One");
        if (Input.GetKey(KeyCode.E))
            SceneManager.LoadScene("Main_Menu");
    }
}
 