using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Selection_Controller_Player2 : MonoBehaviour
{
    public Sprite[] portraits;
    public Sprite[] selectedPortraits;
    public Sprite[] arrows;
    public Image portrait;
    public Image leftArrow;
    public Image rightArrow;
    private int index;
    private float selection;
    private bool pressed;
    private float timer;
    public bool selected;
    void Start()
    {
        index = 0;
        timer = 1f;
    }

    void Update()
    { 
        if (!selected)
        {
            timer += Time.deltaTime;
            portrait.sprite = portraits[index];
            if (timer > 0.5f)
            {
                selection = Input.GetAxisRaw("Player2_Rotation");
                if (selection > 0)
                {
                    if (FindObjectOfType<Audio_Manager>())
                        FindObjectOfType<Audio_Manager>().Play("Player2_Click");
                    index++;
                    rightArrow.sprite = arrows[1];
                }
                else if (selection < 0)
                {
                    if (FindObjectOfType<Audio_Manager>())
                        FindObjectOfType<Audio_Manager>().Play("Player2_Click");
                    index--;
                    leftArrow.sprite = arrows[1];
                }
                else
                {
                    leftArrow.sprite = arrows[0];
                    rightArrow.sprite = arrows[0];
                }
                if (index == portraits.Length)
                    index = 0;
                else if (index == -1)
                    index = portraits.Length - 1;
                if (selection != 0)
                    timer = 0f;
            }
            if (Input.GetKey(KeyCode.Keypad0) || Input.GetKey(KeyCode.Joystick2Button1))
            {
                leftArrow.sprite = arrows[0];
                rightArrow.sprite = arrows[0];
                selected = true;
                playSelected();
            }
        }
        else
        {
            if (index == 0)
                PlayerPrefs.SetInt("Player2_Character", 0);
            else if (index == 1)
                PlayerPrefs.SetInt("Player2_Character", 1);
            else if (index == 2)
                PlayerPrefs.SetInt("Player2_Character", 2);
            else if (index == 3)
                PlayerPrefs.SetInt("Player2_Character", 3);
            leftArrow.sprite = arrows[0];
            rightArrow.sprite = arrows[0];

            portrait.sprite = selectedPortraits[index];
        }
    }
    private void playSelected()
    {
        if (FindObjectOfType<Audio_Manager>())
        {
            if (index == 0)
                FindObjectOfType<Audio_Manager>().Play("Player2_Brutus_Pick");
            else if (index == 1)
                FindObjectOfType<Audio_Manager>().Play("Player2_Aurelia_Pick");
            else if (index == 2)
                FindObjectOfType<Audio_Manager>().Play("Player2_Albus_Pick");
            else if (index == 3)
                FindObjectOfType<Audio_Manager>().Play("Player2_Nubia_Pick");
        }
    }

    private void stopSelected()
    {
        if (FindObjectOfType<Audio_Manager>())
        {
            if (index == 0)
                FindObjectOfType<Audio_Manager>().Stop("Player2_Brutus_Pick");
            else if (index == 1)
                FindObjectOfType<Audio_Manager>().Stop("Player2_Aurelia_Pick");
            else if (index == 2)
                FindObjectOfType<Audio_Manager>().Stop("Player2_Albus_Pick");
            else if (index == 3)
                FindObjectOfType<Audio_Manager>().Stop("Player2_Nubia_Pick");
        }
    }
}
