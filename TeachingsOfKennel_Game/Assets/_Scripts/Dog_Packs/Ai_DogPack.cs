using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_DogPack : DogPack
{

    private void Update()
    {
        Collider2D collision = Physics2D.OverlapCircle(this.transform.position, 2);
        if (collision != null && Game_Engine.instance.GetState() != State.fight)
        {
            GlobalEventSystem.instance.packDetection(collision.tag, this); 
        }


        SetPos(); 
    }
}
