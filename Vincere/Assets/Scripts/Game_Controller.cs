using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    private GameObject playerOne;
    private GameObject playerTwo;
    private Car_Controller[] playerScripts;
    public Lap_Counter[] lapHandlers;
    private bool gameOver;
    private float timer;
    private bool raceStarted;
    public Text[] countdown;
    
    private void Start()
    {
        playerScripts = new Car_Controller[2];
        playerOne = GameObject.FindGameObjectWithTag("Player1");
        playerScripts[0] = playerOne.GetComponent<Player_One>();
        playerTwo = GameObject.FindGameObjectWithTag("Player2");
        playerScripts[1] = playerTwo.GetComponent<Player_Two>();

        if (FindObjectOfType<Audio_Manager>())
        {
            FindObjectOfType<Audio_Manager>().ResetSounds();
            FindObjectOfType<Audio_Manager>().Play("Crowd_In_Game");
        }

        for(int i=0; i < playerScripts.Length; i++)
        {
            playerScripts[i].setActivity(false);
        }

        countdown[0].text = "III";
        countdown[1].text = "III";
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            countdown[0].text = "II";
            countdown[1].text = "II";
        }
        if (timer > 2f)
        {
            countdown[0].text = "I";
            countdown[1].text = "I";
        }

        if (timer > 3f && !raceStarted)
        {
            countdown[0].enabled = false;
            countdown[1].enabled = false;
            foreach (Car_Controller playerScript in playerScripts)
            {
                playerScript.setActivity(true);
            }
            raceStarted = true;
        }
        foreach(Car_Controller playerScript in playerScripts)
        {
            if (playerScript.checkWin())
            {
                playerScripts[0].setActivity(false);
                playerScripts[1].setActivity(false);
                gameOver = true;
            }
        }
        if(lapControl())
            if (FindObjectOfType<Audio_Manager>())
                if (!FindObjectOfType<Audio_Manager>().isPlaying("Drum_Roll"))
                    FindObjectOfType<Audio_Manager>().Play("Drum_Roll");

        if (gameOver)
            StartCoroutine(endGame());
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("Level_One");
        if (Input.GetKey(KeyCode.E))
            SceneManager.LoadScene("Main_Menu");
    }

    IEnumerator endGame()
    {
        if (FindObjectOfType<Audio_Manager>())
            if(!FindObjectOfType<Audio_Manager>().isPlaying("Game_Over"))
                FindObjectOfType<Audio_Manager>().Play("Game_Over");
        foreach (Car_Controller playerScript in playerScripts)
        {
            playerScript.setActivity(false);
        }
        yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("End_Screen");
    }

    private bool lapControl()
    {
        foreach (Car_Controller playerScript in playerScripts)
        {
            if(playerScript.lapCount == 4)
            {
                return true;
            }
        }
        return false;
    }
}