using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Dog_Chihuahua : DogBase
{
    private bool cd = true; 

    public override void Bark(DogPack target)
    {
        if (cd){
            cd = false;
            StartCoroutine(Barks(target)); 
        }
        else{
            cd = true;
            target.SetFaith(-barkStrength);
        }
    }

    private IEnumerator Barks(DogPack target) {
       float newBarkStrength = barkStrength - (barkStrength / 4); 
       target.SetFaith(-newBarkStrength);
       yield return new WaitForSeconds(0.2f);
       target.SetFaith(-newBarkStrength);
       yield return new WaitForSeconds(0.2f);
       target.SetFaith(-newBarkStrength);
    }
}
