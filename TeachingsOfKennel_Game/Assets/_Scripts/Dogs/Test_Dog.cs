using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Dog : DogBase
{
    public override void bark(DogPack target)
    {
        print(dogFaith); 
        target.SetFaith(-barkStrength);
    }
}
