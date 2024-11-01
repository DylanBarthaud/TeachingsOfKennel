using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Rotweiler : DogBase
{
    private int cd = 3;

    public override void Bark(DogPack target)
    {
        if(cd == 0)
        {
            cd = 3;
            target.SetFaith(-barkStrength * 3);
        }
        else
        {
            cd--;
            base.Bark(target);
        }
    }
}
