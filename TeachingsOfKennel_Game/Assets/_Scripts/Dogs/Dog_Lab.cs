using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Lab : DogBase
{
    public override void Bark(DogPack dogPack, DogPack target)
    {
        dogPack.SetFaith((dogFaith / 10) * 4);
        target.SetFaith(-barkStrength);
    }
}
