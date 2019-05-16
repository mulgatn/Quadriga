using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End_Game_Controller : MonoBehaviour
{
    public Sprite[] playerOneWin;
    public Sprite[] playerOneLose;
    public Sprite[] playerTwoWin;
    public Sprite[] playerTwoLose;
    public Image playerOne;
    public Image playerTwo;

    private void Start()
    {
        if (FindObjectOfType<Audio_Manager>())
            FindObjectOfType<Audio_Manager>().ResetSounds();
        if(PlayerPrefs.GetInt("Winner") == 1)
        {
            playerOne.sprite = playerOneWin[PlayerPrefs.GetInt("Player1_Character")];
            playerTwo.sprite = playerTwoLose[PlayerPrefs.GetInt("Player2_Character")];
        }
        else
        {
            playerOne.sprite = playerOneLose[PlayerPrefs.GetInt("Player1_Character")];
            playerTwo.sprite = playerTwoWin[PlayerPrefs.GetInt("Player2_Character")];
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("Main_Menu");
    }
}
