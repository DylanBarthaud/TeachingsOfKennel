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
                Game_Engine.instance.StartDogFight(this, detectedPack);
                SetState(State.fight); 
                detectedPack.SetState(State.fight);
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