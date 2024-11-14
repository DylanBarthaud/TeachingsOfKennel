using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_GermanShep : DogBase
{
    public override void Bark(DogPack dogPack, DogPack target)
    {
        List<DogBase> dogs = dogPack.GetActiveDogs();
        DogBase nextDog;
        if (dogPack.GetDogindex() < dogs.Count)
        {
            nextDog = dogs[dogPack.GetDogindex()];
        }
        else { nextDog = dogs[0]; }

        nextDog.AddToBarkStrength(5); 
        nextDog.AddToBarkSpeed(0.5f);
        nextDog.AddToFaith(5);
        target.SetFaith(-barkStrength); 
    }
}
