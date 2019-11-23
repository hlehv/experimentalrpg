using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBall : MonoBehaviour
{
    public bool isActive;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = activeSprite;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
        }

    }
}
