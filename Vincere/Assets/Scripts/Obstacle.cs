using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector2 minSpawnPoint;
    public Vector2 maxSpawnPoint;
    private float timer;
    public float blinkTimer;
    public float waitTime;
    private float waitTimer;
    private new EdgeCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        waitTimer += Time.deltaTime;
        if (waitTimer < waitTime)
        {
            if (timer > blinkTimer)
            {
                if (!GetComponent<Renderer>().enabled)
                    GetComponent<Renderer>().enabled = true;
                else
                    GetComponent<Renderer>().enabled = false;
                timer = 0;
            }
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
            collider.enabled = true;
        }
    }

    public void reSpawn()
    {
        transform.position = getRandomSpawn();
        transform.eulerAngles = getRandomRotation();
        timer = 0;
        waitTimer = 0;
        collider.enabled = false;
    }

    public Vector2 getRandomSpawn()
    {
        return new Vector2(Random.Range(minSpawnPoint.x, maxSpawnPoint.x), Random.Range(minSpawnPoint.y, maxSpawnPoint.y));
    }   

    public Vector3 getRandomRotation()
    {  
        return new Vector3(0, 0, Random.Range(0f, 360f));
    }
}
