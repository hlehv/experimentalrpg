
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OverworldPlayerController : MonoBehaviour
{
    public Dungeon selected;
    Dungeon movingTo;
    Transform trans;
    bool moving;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        trans = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector3 destination = movingTo.GetComponent<Transform>().position;
            Vector3 dir = new Vector3(destination.x - trans.position.x, destination.y - trans.position.y, 0);
            trans.position += dir * speed;
            if (Math.Abs(dir.magnitude) < 0.5)
            {
                selected = movingTo;
                movingTo = null;
                moving = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
                movingTo = selected.neighbors[0];
            else if (Input.GetKeyDown(KeyCode.S))
                movingTo = selected.neighbors[1];
            else if (Input.GetKeyDown(KeyCode.A))
                movingTo = selected.neighbors[2];
            else if (Input.GetKeyDown(KeyCode.D))
                movingTo = selected.neighbors[3];

            if (Input.anyKeyDown && !movingTo.isLocked)
            {
                moving = true;
            }
        }
    }
}