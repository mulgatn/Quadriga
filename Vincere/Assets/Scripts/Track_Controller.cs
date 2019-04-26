using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track_Controller : MonoBehaviour
{
    public GameObject[] tracks;
    public GameObject player;
    public GameObject temp;

    private float halfLength;

    private void Start()
    {
        halfLength = tracks[1].GetComponent<SpriteRenderer>().bounds.max.y - tracks[1].GetComponent<SpriteRenderer>().bounds.center.y;
    }

    private void Update()
    {
        if (player.transform.position.y > tracks[1].GetComponent<SpriteRenderer>().bounds.max.y)
            ForwardSwap();
        if (player.transform.position.y < tracks[1].GetComponent<SpriteRenderer>().bounds.min.y)
            BackwardSwap();
    }

    private void ForwardSwap()
    {
        tracks[0].transform.position = new Vector2(tracks[0].transform.position.x, tracks[2].GetComponent<SpriteRenderer>().bounds.max.y + halfLength);
        temp = tracks[0];
        tracks[0] = tracks[1];
        tracks[1] = tracks[2];
        tracks[2] = temp;
    }

    private void BackwardSwap()
    {
        tracks[2].transform.position = new Vector2(tracks[2].transform.position.x, tracks[0].GetComponent<SpriteRenderer>().bounds.min.y - halfLength);
        temp = tracks[2];
        tracks[2] = tracks[1];
        tracks[1] = tracks[0];
        tracks[0] = temp;
    }
}
