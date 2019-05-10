using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu_Controller : MonoBehaviour
{
    public Image[] startTexts;
    public Canvas[] myCanvases;
    private bool pressedEnter;

    private Vector2 start;
    private Vector2 middle1;
    private Vector2 middle2;
    private Vector2 end;

    public Vector2 addedMiddle1;
    public Vector2 addedMiddle2;
    public Vector2 addedEnd;

    private float timer;
    public float travelTime;

    private void Awake()
    {
        PlayerPrefs.SetInt("Player1_Character", 1);
        PlayerPrefs.SetInt("Player2_Character", 1);
    }

    private void Start()
    {
        FindObjectOfType<Audio_Manager>().Play("Main_Menu");
        FindObjectOfType<Audio_Manager>().Play("Crowd_Main_Menu");

        start = startTexts[0].transform.position;
        middle1 = start + addedMiddle1;
        middle2 = start + addedMiddle2;
        end = start + addedEnd;
    }

    void Update()
    {
        if (pressedEnter)
        {
            timer += Time.deltaTime;
            StartGame();
        }
        if (Input.GetKey(KeyCode.Return))
            pressedEnter = true;
        if(pressedEnter)
            FindObjectOfType<Audio_Manager>().fadeOut("Crowd_Main_Menu", timer / travelTime);
        if (timer / travelTime > 1f)
        {
            FindObjectOfType<Audio_Manager>().Stop("Crowd_Main_Menu");
            SceneManager.LoadScene("Character_Selection");
        }
            

    }

    private void StartGame()
    {
        foreach (Image startText in startTexts)
            startText.transform.position = SecondEaseOut(start, middle1, middle2, end, timer / travelTime);
        pressedEnter = true;
    }

    private Vector2 FirstEaseOut(Vector2 start, Vector2 middle, Vector2 end, float time)
    {
        Vector2 temp1 = Vector2.Lerp(start, middle, time);
        Vector2 temp2 = Vector2.Lerp(middle, end, time);

        return Vector2.Lerp(temp1, temp2, time);
    }

    private Vector2 SecondEaseOut(Vector2 start, Vector2 f_middle, Vector2 s_middle, Vector2 end, float time)
    {
        Vector2 temp1 = FirstEaseOut(start, f_middle, s_middle, time);
        Vector2 temp2 = FirstEaseOut(f_middle, s_middle, end, time);

        return Vector2.Lerp(temp1, temp2, time);
    }
}
