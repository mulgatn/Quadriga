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

    private int winner;
    private int playerOneIndex;
    private int playerTwoIndex;
    private bool winPhrase;
    private bool losePhrase;

    private void Start()
    {
        if (FindObjectOfType<Audio_Manager>())
        {
            FindObjectOfType<Audio_Manager>().ResetSounds();
            FindObjectOfType<Audio_Manager>().Play("End_Game");
        }
        winner = PlayerPrefs.GetInt("Winner");
        playerOneIndex = PlayerPrefs.GetInt("Player1_Character");
        playerTwoIndex = PlayerPrefs.GetInt("Player2_Character");
        if (winner == 1)
        {
            playerOne.sprite = playerOneWin[playerOneIndex];
            playerTwo.sprite = playerTwoLose[playerTwoIndex];
        }
        else
        {
            playerOne.sprite = playerOneLose[playerOneIndex];
            playerTwo.sprite = playerTwoWin[playerTwoIndex];
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("Main_Menu");
        StartCoroutine(playPhrases());
    }

    IEnumerator playPhrases()
    {
        yield return new WaitForSeconds(0.5f);
        //Play win phrase
        if (FindObjectOfType<Audio_Manager>() && !winPhrase)
        {
            winPhrase = true;
            if (winner == 1)
            {
                if (playerOneIndex == 0)
                    FindObjectOfType<Audio_Manager>().Play("Player1_Brutus_Win");
                else if (playerOneIndex == 1)
                    FindObjectOfType<Audio_Manager>().Play("Player1_Aurelia_Win");
                else if (playerOneIndex == 2)
                    FindObjectOfType<Audio_Manager>().Play("Player1_Albus_Win");
                else if (playerOneIndex == 3)
                    FindObjectOfType<Audio_Manager>().Play("Player1_Nubia_Win");
            }
            else if(winner == 2)
            {
                if (playerTwoIndex == 0)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Brutus_Win");
                else if (playerTwoIndex == 1)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Aurelia_Win");
                else if (playerTwoIndex == 2)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Albus_Win");
                else if (playerTwoIndex == 3)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Nubia_Win");
            }
        }
         
        yield return new WaitForSeconds(3f);
        //Play lose phrase

        if (FindObjectOfType<Audio_Manager>() && !losePhrase)
        {
            losePhrase = true;
            if (winner == 2)
            {
                if (playerOneIndex == 0)
                    FindObjectOfType<Audio_Manager>().Play("Player1_Brutus_Lose");
                else if (playerOneIndex == 1)
                    FindObjectOfType<Audio_Manager>().Play("Player1_Aurelia_Lose");
                else if (playerOneIndex == 2)
                    FindObjectOfType<Audio_Manager>().Play("Player1_Albus_Lose");
                else if (playerOneIndex == 3)
                    FindObjectOfType<Audio_Manager>().Play("Player1_Nubia_Lose");
            }
            else if (winner == 1)
            {
                if (playerTwoIndex == 0)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Brutus_Lose");
                else if (playerTwoIndex == 1)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Aurelia_Lose");
                else if (playerTwoIndex == 2)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Albus_Lose");
                else if (playerTwoIndex == 3)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Nubia_Lose");
            }
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Main_Menu");
    }
}
