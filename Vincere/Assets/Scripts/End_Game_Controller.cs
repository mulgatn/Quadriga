using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End_Game_Controller : MonoBehaviour
{
    public Text playerOneText;
    public Text playerTwoText;

    private void Start()
    {
       if(PlayerPrefs.GetInt("Winner") == 1)
        {
            playerOneText.text = playerOneText.text +  " WON!";
            playerTwoText.text = playerTwoText.text + " LOST...";
        }
        else
        {
            playerOneText.text = playerOneText.text + " LOST...";
            playerTwoText.text = playerTwoText.text + " WON!";
        }
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("Main_Menu");
    }
}
