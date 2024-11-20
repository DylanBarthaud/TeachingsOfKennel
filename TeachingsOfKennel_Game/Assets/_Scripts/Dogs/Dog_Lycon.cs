using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Lycon : DogBase
{
    public override void Bark(DogPack dogPack, DogPack target){
        int r = Random.Range(1, 101);
        target.SetFaith(-barkStrength);
        if (r >= 75){
            List<DogBase> list  = target.GetActiveDogs();
            int x = Random.Range(0, list.Count);
            target.ConvertDog(list[x], dogPack); 
        }
    }
}
