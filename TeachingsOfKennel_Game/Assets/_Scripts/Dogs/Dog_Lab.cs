using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Lab : DogBase
{
    private bool cd = false;

    public override void Bark(DogPack target)
    {
        if (cd){
            cd = false;
            dogFaith = dogFaith * 2; 
        }
        else
        {
            cd = true;
            dogFaith = dogFaith / 2;
        }

        target.SetFaith(-barkStrength);
    }


}
