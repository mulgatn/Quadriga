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


    private void Awake()
    {
        currentLap = "";
        lapIndicator = 0;
    }

    private void Update()
    {
        currentLap = laps[lapIndicator];
        currentLapText.text = currentLap;
    }
    public void nextLap()
    {
        lapIndicator++;
        if (lapIndicator == 7)
            lapIndicator = 6;
    }
}