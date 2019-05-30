using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Selection_Controller : MonoBehaviour
{
    public Character_Selection_Controller_Player1 playerOne;
    public Character_Selection_Controller_Player2 playerTwo;
    private float timer;
    public float waitTime;

    private void Start()
    {
        if (FindObjectOfType<Audio_Manager>())
        {
            FindObjectOfType<Audio_Manager>().Stop("Crowd_Main_Menu");
            FindObjectOfType<Audio_Manager>().Stop("Player1_Horse");
            FindObjectOfType<Audio_Manager>().Stop("Player2_Horse");
            FindObjectOfType<Audio_Manager>().setVolume("Main_Menu", 0.6f);
        } 
           
    }

    private void Update()
    {
        if (playerOne.selected && playerTwo.selected)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
                SceneManager.LoadScene("Loading");        
        }
        else
            timer = 0;
    }

}
