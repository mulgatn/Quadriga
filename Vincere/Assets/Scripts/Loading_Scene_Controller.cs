using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading_Scene_Controller : MonoBehaviour
{
    public Image playerOneBar;
    public Image playerTwoBar;

    public Sprite[] playerOneBarSprites;
    public Sprite[] playerTwoBarSprites;

    private int index;
    private float time;
    public float timeToSwitch;

    void Start()
    {
        playerOneBar.sprite = playerOneBarSprites[0];
        playerTwoBar.sprite = playerTwoBarSprites[0];
    }

    
    void Update()
    {
        time += Time.deltaTime;

        if(time > timeToSwitch)
        {
            if (index == 10)
                SceneManager.LoadScene("Level_One");
            else
            {
                index++;
                playerOneBar.sprite = playerOneBarSprites[index];
                playerTwoBar.sprite = playerTwoBarSprites[index];
            }
            time = 0f;
        }   
    }
}
