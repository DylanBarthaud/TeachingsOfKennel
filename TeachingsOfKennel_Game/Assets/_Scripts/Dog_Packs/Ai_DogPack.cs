using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_DogPack : DogPack
{
    private int minRange = -2; 
    private int maxRange = 2;

    private float timeSinceMoved = 0; 

    private void Update()
    {
        Collider2D collision = Physics2D.OverlapCircle(this.transform.position, 2);
        if (collision != null && Game_Engine.instance.GetState() != State.fight)
        {
            GlobalEventSystem.instance.packDetection(collision.gameObject.GetComponent<IHasId>().GetId(), this); 
        }

        SetPos(); 

        if(timeSinceMoved <= 0)
        {
            MoveFlag(Game_Engine.instance.GetNewPos(minRange,maxRange)); 
            timeSinceMoved = 1;
        }

        timeSinceMoved -= Time.deltaTime;
    }
}


