using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_DogPack : DogPack
{
    [SerializeField] private Game_Engine engine; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        engine.StartDogFight(this, collision.GetComponent<DogPack>()); 
    }
}
