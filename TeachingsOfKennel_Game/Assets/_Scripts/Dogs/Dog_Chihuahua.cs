using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Chihuahua : DogBase
{
    public override void Bark(DogPack dogPack, DogPack target)
    {
        List<DogBase> dogs = dogPack.GetActiveDogs();
        DogBase nextDog;
        if (dogPack.GetDogindex() < dogs.Count){
            nextDog = dogs[dogPack.GetDogindex()];
        }
        else { nextDog = dogs[0]; }

        if (nextDog.GetBreedId() == 2){
            nextDog.SetBarkSpeed(GetBaseBarkSpeed() / 2); 
        }
        else{
            float x = nextDog.GetBaseBarkSpeed() - GetBaseBarkSpeed();
            if (x < 0) { x = 0;}
            nextDog.AddToBarkSpeed(x / 2); 
        }

        target.SetFaith(-barkStrength);
    }
}
