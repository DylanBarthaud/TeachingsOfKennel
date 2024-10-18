using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DogBase : MonoBehaviour 
{
    [SerializeField] protected int id;

    [SerializeField] protected Dog_Graphic graphic;

    protected float dogFaith;
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
        return id;
    }
}

