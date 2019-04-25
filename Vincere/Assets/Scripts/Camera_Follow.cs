using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed;

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, rotationSpeed);
    }
}