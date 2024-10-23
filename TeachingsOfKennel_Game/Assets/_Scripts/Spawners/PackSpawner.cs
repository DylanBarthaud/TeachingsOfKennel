using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private DogSpawner dogSpawner;
    [SerializeField] private GameObject packTemplate; 
    [SerializeField] private int minRange, maxRange; 

    private int numOfPacks = 0;

    public void SpawnObject(GameObject caller, int numberOfItems)
    {
        dogSpawner.GetTargetPack(SpawnPack()); 
        dogSpawner.SpawnObject(this.gameObject, numberOfItems);
        
    }

    public void SpawnObject(GameObject caller, int[] listOfItemIds)
    {
        dogSpawner.GetTargetPack(SpawnPack());
        dogSpawner.SpawnObject(this.gameObject, listOfItemIds);
    }

    private DogPack SpawnPack()
    {
        DogPack newPack = Instantiate(packTemplate.GetComponent<DogPack>(), Game_Engine.instance.GetNewPos(minRange, maxRange), transform.rotation);
        newPack.SetId(numOfPacks);
        numOfPacks++;
        return newPack;
    }
}
