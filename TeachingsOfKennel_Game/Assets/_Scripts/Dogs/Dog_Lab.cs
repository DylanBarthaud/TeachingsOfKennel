using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Lab : DogBase
{
    public override IEnumerator Bark(DogPack dogPack, DogPack target)
    {
        yield return new WaitForSeconds(barkSpeed);
        print(dogName + ": BARK");
        target.SetFaith(-barkStrength);
        dogPack.TickBarks(target);
    }
}
