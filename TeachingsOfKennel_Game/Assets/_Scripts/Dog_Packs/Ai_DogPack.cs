using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_DogPack : DogPack
{
    private Vector2 startPos; 
    private Vector2 range = new Vector2(0, 0);
    private float timeSinceMoved = 5; 

    private void Update()
    {
        Collider2D collision = Physics2D.OverlapCircle(this.transform.position, 2);
        if (collision != null && collision.gameObject.GetComponent<DogPack>() != null){
            DogPack detectedPack = collision.gameObject.GetComponent<DogPack>();
            if (detectedPack.GetState() == State.freeRoam && GetState() == State.freeRoam && packId > detectedPack.GetId()){
                TickBarks(detectedPack);
                detectedPack.TickBarks(this); 
            }
        }
        
        SetPos(); 

        if(timeSinceMoved <= 0)
        {
            MoveFlag(utilities.GetNewPos(range, startPos)); 
            timeSinceMoved = 3;
        }
        timeSinceMoved -= Time.deltaTime;
    }

    public void SetRoamDistance(Vector2 startPos, float setRange){
        this.startPos = startPos; 
        range = new Vector2(-setRange, setRange); 
    }
}