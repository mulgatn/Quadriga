using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float followSpeed;
    Vector2 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.transform.rotation;
        if (player.GetComponent<Car_Controller>().breaking)
            transform.position = Vector2.Lerp(transform.position, player.transform.position + player.transform.up * distance, followSpeed * Time.deltaTime);
        else
            transform.position = Vector2.Lerp(transform.position, player.transform.position + player.transform.up * -distance, followSpeed * followSpeed * Time.deltaTime);
    }
}
