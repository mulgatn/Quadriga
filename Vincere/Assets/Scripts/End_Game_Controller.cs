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
    public List<string> Player1AlbusWins;
    public List<string> Player2AlbusWins;
    public List<string> Player1BrutusLose;
    public List<string> Player2BrutusLose;
    public Image playerOne;
    public Image playerTwo;

    private int winner;
    private int playerOneIndex;
    private int playerTwoIndex;
    private bool winPhrase;
    private bool losePhrase;

    private void Awake()
    {
        //Player1 Albus Win Phrases
        {
            Player1AlbusWins.Add("Player1_Albus_Win_1");
            Player1AlbusWins.Add("Player1_Albus_Win_2");
            Player1AlbusWins.Add("Player1_Albus_Win_3");
            Player1AlbusWins.Add("Player1_Albus_Win_4");
            Player1AlbusWins.Add("Player1_Albus_Win_5");
        }

        //Player2 Albus Win Phrases
        {
            Player2AlbusWins.Add("Player2_Albus_Win_1");
            Player2AlbusWins.Add("Player2_Albus_Win_2");
            Player2AlbusWins.Add("Player2_Albus_Win_3");
            Player2AlbusWins.Add("Player2_Albus_Win_4");
            Player2AlbusWins.Add("Player2_Albus_Win_5");
        }

        //Player1 Brutus Lose Phrases
        {
            Player1BrutusLose.Add("Player1_Brutus_Lose_1");
            Player1BrutusLose.Add("Player1_Brutus_Lose_2");
            //Pitiful
            //Player1BrutusLose.Add("Player1_Brutus_Lose_3");
        }

        //Player2 Brutus Lose Phrases
        {
            Player2BrutusLose.Add("Player2_Brutus_Lose_1");
            Player2BrutusLose.Add("Player2_Brutus_Lose_2");
            //Pitiful
            //Player2BrutusLose.Add("Player2_Brutus_Lose_3");
        }
    }

    private void Start()
    {
        if (FindObjectOfType<Audio_Manager>())
            FindObjectOfType<Audio_Manager>().ResetSounds();
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
        yield return new WaitForSeconds(1f);
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
                    FindObjectOfType<Audio_Manager>().Play(Player1AlbusWins[Random.Range(0,Player1AlbusWins.Count)]);
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
                    FindObjectOfType<Audio_Manager>().Play(Player2AlbusWins[Random.Range(0, Player2AlbusWins.Count)]);
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
                    FindObjectOfType<Audio_Manager>().Play(Player1BrutusLose[Random.Range(0, Player1BrutusLose.Count)]);
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
                    FindObjectOfType<Audio_Manager>().Play(Player2BrutusLose[Random.Range(0, Player2BrutusLose.Count)]);
                else if (playerTwoIndex == 1)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Aurelia_Lose");
                else if (playerTwoIndex == 2)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Albus_Lose");
                else if (playerTwoIndex == 3)
                    FindObjectOfType<Audio_Manager>().Play("Player2_Nubia_Lose");
            }
        }

    }
}
