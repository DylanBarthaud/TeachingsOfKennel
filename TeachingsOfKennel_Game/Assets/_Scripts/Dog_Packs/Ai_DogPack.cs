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
        if (collision != null)
        {
            DogPack detectedPack = collision.gameObject.GetComponent<DogPack>();
            if (detectedPack.GetState() == State.freeRoam && GetState() == State.freeRoam && packId > detectedPack.GetId())
            {
                TickBarks(detectedPack);
                detectedPack.TickBarks(this); 
            }
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