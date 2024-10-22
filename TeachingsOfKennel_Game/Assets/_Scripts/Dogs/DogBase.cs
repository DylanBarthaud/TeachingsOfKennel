using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DogBase : MonoBehaviour 
{
    [SerializeField] protected int breedId;
    protected int personalId; 

    [SerializeField] protected Dog_Graphic graphic;

    protected float dogFaith = 12f; 
    protected float dogSpeed;
    protected int barkStrength = 10;

    protected float barkCoolDown;
    protected float barkCurrentCoolDown;

    public abstract void bark(DogPack target);

    public float GetFaith() {
        return dogFaith; 
    }

    public Dog_Graphic GetGraphic() { 
        return graphic;
    }

    public float GetSpeed() {
        return dogSpeed; 
    }

    public int GetId() { 
        return personalId;
    }

    public void SetId(int id){
        personalId = id;
    }
}

