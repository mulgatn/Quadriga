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
    public Vector2 start;
    public Vector2 middle;
    public Vector2 end;

    float timer;
    public float travelTime;

    private void Start()
    {
        timer = 0;
    }


    private void Update()
    {
        timer += Time.deltaTime;
        startTexts[0].transform.position = QuadraticCurve(start, middle, end, timer / travelTime);
    }


    public Vector2 QuadraticCurve(Vector2 start, Vector2 middle, Vector2 end, float time)
    {
        return Vector2.Lerp(Vector2.Lerp(start, middle, time), Vector2.Lerp(middle, end, time), time);
    }
}
