using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DogBase : MonoBehaviour
{
    protected float dogFaith;
    protected int barkStrength;

    protected float barkCoolDown;
    protected float barkCurrentCoolDown;

    protected Sprite icon;

    public abstract void bark();
    public float GetFaith() {
        return dogFaith; 
    }
}
