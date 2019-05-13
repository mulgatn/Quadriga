using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lap_Counter : MonoBehaviour
{
    private string[] laps = { "I", "II", "III", "IV", "V", "VI", "VII" };
    public Text currentLapText;
    private int lapIndicator;
    private string currentLap;
    private Vector3 inScene;
    private Vector3 outScene;
    private float time;
    public float travelTime;

    private bool goingIn;
    private bool goingOut;

    private void Awake()
    {
        currentLap = laps[0];
        inScene = transform.position;
        outScene = new Vector2(inScene.x - 275f, inScene.y);
        Debug.Log(transform.position);
        transform.position = outScene;
    }

    private void Update()
    {
        currentLap = laps[lapIndicator];
        currentLapText.text = currentLap;
        setLap();
    }

    public void setLap()
    {
        if(goingIn)
        {
            time += Time.deltaTime;
            transform.position = Vector2.Lerp(outScene, inScene, time / travelTime);
            if (time / travelTime > 1f)
            {
                goingIn = false;
                goingOut = true;
                time = 0f;
            }
        }        
        if(goingOut)
        {
            time += Time.deltaTime;
            transform.position = Vector2.Lerp(inScene, outScene, time / travelTime);
            if (time / travelTime > 1f)
            {
                if (goingOut)
                    lapIndicator++;
                goingIn = false;
                goingOut = false;
                time = 0f;
            }
        }           
    }

    public void nextLap()
    {
        goingIn = true;
    }
}