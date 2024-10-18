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

    public event Action<String, DogPack> onPackDetection; 

    public void packDetection(String tag, DogPack attacker)
    {
        if(onPackDetection != null)
        {
            onPackDetection(tag, attacker);
        }
    }
}
