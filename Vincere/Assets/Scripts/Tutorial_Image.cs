using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Image : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationSpeed;
    private float timer;
    private Image spriteRenderer;
    private int index = 1;

    void Start()
    {
        spriteRenderer = GetComponent<Image>();
        spriteRenderer.sprite = sprites[0];
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > animationSpeed)
        {
            if (index == 2)
                index = 0;
            spriteRenderer.sprite = sprites[index];
            index++;
            timer = 0f;
        }
    }
}
