using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float followSpeed;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y, -5f);
        transform.rotation = player.transform.rotation;
            if (player.GetComponent<Car_Controller>().breaking)
                transform.position = Vector3.Lerp(transform.position, target + player.transform.up * distance, followSpeed * Time.deltaTime);
            else
                transform.position = Vector3.Lerp(transform.position, target + player.transform.up * -distance, followSpeed * followSpeed * Time.deltaTime);
       //transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
    }
}
