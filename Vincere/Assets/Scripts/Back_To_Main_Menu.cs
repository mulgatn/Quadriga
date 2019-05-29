using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Back_To_Main_Menu : MonoBehaviour
{
    public Text[] mainMenuCountDown;
    public float timer;
    private float time;

    private void Start()
    {
        time = timer;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            time = timer;
            for (int i = 0; i < mainMenuCountDown.Length; i++)
                mainMenuCountDown[i].text = "";
        }
            
        else
        {
            time -= Time.deltaTime;
            if (time < 5f)
                for (int i = 0; i < mainMenuCountDown.Length; i++)
                    mainMenuCountDown[i].text = "Going Back To Main Menu in " + "\n" + Mathf.CeilToInt(time);
            if (time < 0f)
                SceneManager.LoadScene("Main_Menu");
        }  
    }
}
