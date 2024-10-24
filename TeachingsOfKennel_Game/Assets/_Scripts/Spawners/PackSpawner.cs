using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private DogSpawner dogSpawner;
    [SerializeField] private GameObject packTemplate;
    [SerializeField] private GameObject playerPackTemplate; 
    [SerializeField] private int minRange, maxRange; 

    private int numOfPacks = 0;

    public void SpawnObject(GameObject caller, int numberOfItems)
    {
        DogPack newPack;

        if (numOfPacks == 0){
            newPack = SpawnPlayer(); 
        }
        else{
            newPack = SpawnPack();
        }

        dogSpawner.GetTargetPack(newPack); 
        dogSpawner.SpawnObject(this.gameObject, numberOfItems);
        
    }

    public void SpawnObject(GameObject caller, int[] listOfItemIds)
    {
        DogPack newPack;

        if (numOfPacks == 0)
        {
            newPack = SpawnPlayer();
        }
        else
        {
            newPack = SpawnPack();
        }

        dogSpawner.GetTargetPack(newPack);
        dogSpawner.SpawnObject(this.gameObject, listOfItemIds);
    }

    private DogPack SpawnPack()
    {
        DogPack newPack = Instantiate(packTemplate.GetComponent<DogPack>(), Game_Engine.instance.GetNewPos(minRange, maxRange), transform.rotation);
        newPack.SetId(numOfPacks);
        numOfPacks++;
        return newPack;
    }

    private DogPack SpawnPlayer()
    {
        Vector2 pos = new Vector2(0, 0);
        DogPack newPack = Instantiate(playerPackTemplate.GetComponent<DogPack>(), pos, transform.rotation);
        newPack.SetId(numOfPacks);
        numOfPacks++;
        return newPack;
    }
}
