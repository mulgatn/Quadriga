using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Selection_Controller_Player1 : MonoBehaviour
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
    private KeyCode scrollLeft;
    private KeyCode scrollRight;
    private KeyCode scrollLeftAlt;
    private KeyCode scrollRightAlt;
    void Start()
    {
        scrollRight = KeyCode.B;
        scrollLeft = KeyCode.V;
        scrollLeftAlt = KeyCode.Joystick1Button0;
        scrollRightAlt = KeyCode.Joystick1Button3;
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
                if (Input.GetKey(scrollRight) || Input.GetKey(scrollRightAlt))
                {
                    if (FindObjectOfType<Audio_Manager>())
                        FindObjectOfType<Audio_Manager>().Play("Player1_Click");
                    index++;
                    rightArrow.sprite = arrows[1];
                }
                else if (Input.GetKey(scrollLeft) || Input.GetKey(scrollLeftAlt))
                {
                    if (FindObjectOfType<Audio_Manager>())
                        FindObjectOfType<Audio_Manager>().Play("Player1_Click");
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
                timer = 0f;
            }
            if (Input.GetButton("Player1_Rotation") && Input.GetAxisRaw("Player1_Rotation") == 0)
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
                PlayerPrefs.SetInt("Player1_Character", 0);
            else if (index == 1)
                PlayerPrefs.SetInt("Player1_Character", 1);
            else if (index == 2)
                PlayerPrefs.SetInt("Player1_Character", 2);
            else if (index == 3)
                PlayerPrefs.SetInt("Player1_Character", 3);
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
                FindObjectOfType<Audio_Manager>().Play("Player1_Brutus_Pick");
            else if (index == 1)
                FindObjectOfType<Audio_Manager>().Play("Player1_Aurelia_Pick");
            else if (index == 2)
                FindObjectOfType<Audio_Manager>().Play("Player1_Albus_Pick");
            else if (index == 3)
                FindObjectOfType<Audio_Manager>().Play("Player1_Nubia_Pick");
        }
    }

    private void stopSelected()
    {
        if (FindObjectOfType<Audio_Manager>())
        {
            if (index == 0)
                FindObjectOfType<Audio_Manager>().Stop("Player1_Brutus_Pick");
            else if (index == 1)
                FindObjectOfType<Audio_Manager>().Stop("Player1_Aurelia_Pick");
            else if (index == 2)
                FindObjectOfType<Audio_Manager>().Stop("Player1_Albus_Pick");
            else if (index == 3)
                FindObjectOfType<Audio_Manager>().Stop("Player1_Nubia_Pick");
        }
    }
}
