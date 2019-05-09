using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Selection_Controller_Player1 : MonoBehaviour
{
    public Sprite[] portraits;
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

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!selected)
        {
            pressed = Input.GetButtonDown("Player1_Rotation");
            if (timer > 0.3f)
            {
                selection = Input.GetAxisRaw("Player1_Rotation");
                if (selection > 0)
                {
                    index++;
                    rightArrow.sprite = arrows[1];
                }
                else if (selection < 0)
                {
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
            portrait.sprite = portraits[index];
            if (pressed && selection == 0)
                selected = true;
        }
        else
        {
            if (index == 0)
                PlayerPrefs.SetString("Player1_Character", "Brutus");
            else if (index == 1)
                PlayerPrefs.SetString("Player1_Character", "Aurelia");
            else if (index == 2)
                PlayerPrefs.SetString("Player1_Character", "Albus");
            else if (index == 3)
                PlayerPrefs.SetString("Player1_Character", "Nubia");
            if (Input.GetButtonDown("Player1_Rotation") && Input.GetAxisRaw("Player1_Rotation") == 0)
                selected = false;
        }
            
    }
}
