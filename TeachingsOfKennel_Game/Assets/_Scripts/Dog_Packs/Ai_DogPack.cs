using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_DogPack : DogPack
{
    private Vector2 range = new Vector2(-5, 5); 

    private float timeSinceMoved = 0; 

    private void Update()
    {
        Collider2D collision = Physics2D.OverlapCircle(this.transform.position, 2);
        if (collision != null && collision.gameObject.GetComponent<DogPack>().GetState() == State.freeRoam && GetState() == State.freeRoam)
        {
            GlobalEventSystem.instance.packDetection(collision.gameObject.GetComponent<IHasId>().GetId(), this); 
        }

        SetPos(); 

        if(timeSinceMoved <= 0)
        {
            MoveFlag(utilities.GetNewPos(range)); 
            timeSinceMoved = 1;
        }

        timeSinceMoved -= Time.deltaTime;
    }
}


