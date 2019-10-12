using UnityEngine;
using System;
using System.Collections;

public class Float : MonoBehaviour
{
    float originalY;

    public float floatStrength;
    public float floatSpeed;

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time * floatSpeed) * floatStrength),
            transform.position.z);
    }
}