using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horse_animation : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D player;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = transform.root.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            animator.SetFloat("Animation_Speed", player.velocity.magnitude * 1.5f);
    }
}
