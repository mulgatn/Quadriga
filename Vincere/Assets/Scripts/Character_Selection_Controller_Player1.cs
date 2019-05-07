using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Selection_Controller_Player1 : MonoBehaviour
{
    public Sprite[] portraits;
    public Image portrait;
    private int index;
    private float selection;
    private bool pressed;
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        portrait.sprite = portraits[index];

        pressed = Input.GetButtonDown("Player1_Rotation");
        selection = Input.GetAxisRaw("Player1_Rotation");

        if (pressed && selection == 0)
            Debug.Log("Hellooo");

        if (pressed && selection > 0)
            index++;
        else if (pressed && selection < 0)
            index--;
        if (index == portraits.Length)
            index = 0;
        else if (index == -1)
            index = portraits.Length - 1;
    }
}
