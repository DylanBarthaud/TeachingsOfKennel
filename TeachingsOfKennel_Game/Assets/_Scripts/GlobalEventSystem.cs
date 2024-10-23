using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventSystem : MonoBehaviour
{
    public static GlobalEventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action<int, DogPack> onPackDetection; 

    public void packDetection(int packId, DogPack attacker)
    {
        if(onPackDetection != null)
        {
            onPackDetection(packId, attacker);
        }
    }
}
