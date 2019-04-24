using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu_Controller : MonoBehaviour
{
    public Text[] startTexts;
    public Canvas[] myCanvases;
    public float textSpeed;
    private bool pressedEnter;
    
    void Update()
    {

        Debug.Log(myCanvases[0].GetComponent<RectTransform>().rect.height);
        if(!pressedEnter)
        {
            if (Input.GetKey(KeyCode.Return))
                StartGame();
        }
    }

    private void StartGame()
    {
        for (int i = 0; i < 2; i++)
            startTexts[i].transform.position = EaseIn(5, 5, 5, 5);
        pressedEnter = true;
    }

    float EaseIn(float start_value, float target_value, float time_elapsed, float duration)
    {
        time_elapsed /= duration;
        return target_value * time_elapsed * time_elapsed + start_value;
    }
}
