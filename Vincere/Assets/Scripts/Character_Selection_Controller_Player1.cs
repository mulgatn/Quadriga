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
    void Start()
    {
        index = 0;
        timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        portrait.sprite = portraits[index];
        if (Input.GetAxisRaw("Player1_Rotation") > 0)
            rightArrow.sprite = arrows[1];
        else
            rightArrow.sprite = arrows[0];
        if (Input.GetAxisRaw("Player1_Rotation") < 0)
            leftArrow.sprite = arrows[1];
        else
            leftArrow.sprite = arrows[0];

        timer += Time.deltaTime;

        pressed = Input.GetButtonDown("Player1_Rotation");
        if (timer > 0.3f)
        {
            selection = Input.GetAxisRaw("Player1_Rotation");
            if (selection > 0)
                index++;
            else if (selection < 0)
                index--;
            if (index == portraits.Length)
                index = 0;
            else if (index == -1)
                index = portraits.Length - 1;
            if (selection != 0)
                timer = 0f;
        }
        if (pressed && selection == 0)
            Debug.Log("Selected");
    }
}
