using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSpawner : MonoBehaviour, ISpawner
{
    private DogPack targetPack;
    [SerializeField] private DogList dogList; 

    public void SpawnObject(GameObject caller, int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            Vector3 pos = new Vector3(targetPack.transform.position.x, targetPack.transform.position.y, 0);
            GameObject dog = Instantiate(dogList.GetRandomDog(), pos, targetPack.transform.rotation);
            targetPack.AddDog(dog.GetComponent<DogBase>());
        }
    }

    public void SpawnObject(GameObject caller, int[] listOfItemIds)
    {
        for (int i = 0; i < listOfItemIds.Length; i++)
        {
            GameObject dog = dogList.GetDog(listOfItemIds[i]);
            targetPack.AddDog(dog.GetComponent<DogBase>());
        }
    }

    public void GetTargetPack(DogPack target){
        targetPack = target;
    }
}
