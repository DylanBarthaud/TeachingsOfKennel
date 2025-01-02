using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Rotweiler : DogBase
{
    public override void Bark(DogPack dogPack, DogPack target)
    {
        float x = target.GetFaith() / 20; 
        target.SetFaith(-(barkStrength + x));
    }
}
