using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Handler : MonoBehaviour
{
    public Obstacle[] obstacles;

    void Awake()
    {
        foreach(Obstacle o in obstacles)
            o.reSpawn();
    }

    void Update()
    {
        foreach (Obstacle o in obstacles)
        {
            if (!o.gameObject.activeSelf)
            {
                o.reSpawn();
                o.gameObject.SetActive(true);
                o.gameObject.GetComponent<Animator>().SetInteger("Obstacle_Identifier", Random.Range(0, 2));
            }

        }
    }
}
