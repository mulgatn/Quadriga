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
    private Renderer renderer;

    private void Awake()
    {
        collider = GetComponent<EdgeCollider2D>();
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        waitTimer += Time.deltaTime;
        if (waitTimer < waitTime)
        {
            collider.enabled = false;
            if (timer > blinkTimer)
            {
                if (!renderer.enabled)
                    renderer.enabled = true;
                else
                    renderer.enabled = false;
                timer = 0;
            }
        }
        else
        {
            renderer.enabled = true;
            collider.enabled = true;
        }
    }

    public void reSpawn()
    {
        transform.position = getRandomSpawn();
        transform.eulerAngles = getRandomRotation();
        timer = 0;
        waitTimer = 0;
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
