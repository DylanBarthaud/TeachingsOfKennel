using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Chihuahua : DogBase
{
    public override void Bark(DogPack dogPack, DogPack target)
    {
        target.SetFaith(-barkStrength);
    }
}
