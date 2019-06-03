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
    public float cooldown;
    private new BoxCollider2D collider;
    private new Renderer m_renderer;
    private Animator animator;
    public int obstacleID;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        m_renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        obstacleID = Random.Range(0, 2);
        animator.SetInteger("Obstacle_Identifier", obstacleID);
    }

    private void Update()
    {
        obstacleID = Random.Range(0, 2);
        timer += Time.deltaTime;
        waitTimer += Time.deltaTime;
        if(!animator.GetBool("Broken"))
        {
            if (waitTimer < waitTime)
            {
                collider.enabled = false;
                if (timer > blinkTimer)
                {
                    if (!m_renderer.enabled)
                        m_renderer.enabled = true;
                    else
                        m_renderer.enabled = false;
                    timer = 0;
                }
            }
            else
            {
                m_renderer.enabled = true;
                collider.enabled = true;
            }
        }
        else
        {
            collider.enabled = false;
            StartCoroutine(deactivate());
        }
    }

    public void reSpawn()
    {
        transform.position = getRandomSpawn();;
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

    IEnumerator deactivate()
    {
        yield return new WaitForSeconds(cooldown);
        animator.SetBool("Broken", false);
        gameObject.SetActive(false);
    }
}