using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Shake : MonoBehaviour
{
    [HideInInspector]
    public bool shakeReady;

    public float duration;
    private float magnitude;
    public float frequency;

    private float shakeTimer = 0;

    public CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin shaker;

    private void Start()
    {
        if (cam != null)
            shaker = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        magnitude = GetComponent<Player_Movement>().speed;
        if (shakeReady)
            shakeTimer = duration;
        if (cam != null || shaker != null)
        {
            if (shakeTimer > 0)
            {
                Debug.Log("Reached here");
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
