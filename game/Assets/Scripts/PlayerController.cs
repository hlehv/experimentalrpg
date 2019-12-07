using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
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
       if (moving){
           Vector3 destination = movingTo.GetComponent<Transform>().position;
           Vector3 dir = new Vector3(destination.x - trans.position.x, destination.y - trans.position.y, 0);
           trans.position += dir * speed;
           if (Math.Abs(dir.magnitude) < 0.5) {
           		selected = movingTo;
           		movingTo = null;
           		moving = false;
           	}
       }
       else{
           if (Input.GetKeyDown(KeyCode.UpArrow))
               movingTo = selected.neighbors[0];
           else if (Input.GetKeyDown(KeyCode.DownArrow)) 
               movingTo = selected.neighbors[1];
           else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
               movingTo = selected.neighbors[2];
           else if (Input.GetKeyDown(KeyCode.RightArrow)) 
           	   movingTo = selected.neighbors[3];
           else if (Input.GetKeyDown(KeyCode.Return)) {
               foreach (var d in selected.neighbors) {
                if (d) d.isLocked = false;
               }
           }

           if (Input.anyKeyDown && !movingTo.isLocked){ 
               moving = true;
           }
       }
   }
}
