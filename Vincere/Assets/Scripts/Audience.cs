using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    public GameObject[] crowd;
    private int random;
    void Start()
    {
        random = Random.Range(0, 8);
        crowd[random].SetActive(true);
    }
}
