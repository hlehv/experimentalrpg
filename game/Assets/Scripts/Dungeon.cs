using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{

	public List<Dungeon> neighbors;
	public bool isLocked;
	public Sprite unlockedDungeon;
	public Sprite lockedDungeon;
	public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked) sr.sprite = lockedDungeon;
        else sr.sprite = unlockedDungeon;
    }
}
