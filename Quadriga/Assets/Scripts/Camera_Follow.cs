using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject player;
    float followTime = 1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Quaternion.Lerp(transform.rotation, player.transform.rotation, followTime * Time.deltaTime);
    }
}
