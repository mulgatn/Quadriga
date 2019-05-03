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
            playerOneText.text = playerOneText.text +  " Won!";
            playerTwoText.text = playerTwoText.text + " Lost...";
        }
        else
        {
            playerOneText.text = playerOneText.text + " Lost...";
            playerTwoText.text = playerTwoText.text + " Won!";
        }
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("Main_Menu");
    }
}
