using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Shake : MonoBehaviour
{
    [HideInInspector]
    public bool shakeReady;

    private Player_Movement movement;

    public float duration;
    public float magnitude;
    public float frequency;

    public float shakeTimer = 0;

    public CinemachineVirtualCamera cam;
    public CinemachineBasicMultiChannelPerlin shaker;

    private void Start()
    {
        movement = GetComponent<Player_Movement>();
        if (cam != null)
            shaker = cam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();         
    }

    private void Update()
    {
        if (cam != null || shaker != null)
        {
            if (shakeTimer > 0)
            {  
                shaker.m_AmplitudeGain = magnitude;
                shaker.m_FrequencyGain = frequency;

                shakeTimer -= Time.deltaTime;
            }
            else
            {
                shaker.m_AmplitudeGain = 0;
                shakeTimer = 0;
                shakeReady = false;
            }
        }
    }
}
