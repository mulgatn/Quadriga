using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Set : MonoBehaviour
{
    Camera[] myCameras;
    void Start()
    {
        myCameras = new Camera[2];

        myCameras[0] = GameObject.FindGameObjectWithTag("Camera1").GetComponentInChildren<Camera>();
        myCameras[0].targetDisplay = 0;
        Display.displays[0].Activate();
        myCameras[1] = GameObject.FindGameObjectWithTag("Camera2").GetComponentInChildren<Camera>();
        myCameras[1].targetDisplay = 1;
        Display.displays[1].Activate();
    }
}
