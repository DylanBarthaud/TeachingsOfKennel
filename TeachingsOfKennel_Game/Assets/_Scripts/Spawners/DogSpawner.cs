using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSpawner : MonoBehaviour
{
    private DogPack targetPack;
    [SerializeField] private DogList dogList;

    //  SpawnObject (|Number of dogs to spawn|)
    public void SpawnObject(int numberToSpawn){
        for (int i = 0; i < numberToSpawn; i++){
            Vector3 pos = new Vector3(targetPack.transform.position.x, targetPack.transform.position.y, 0);
            GameObject dog = Instantiate(dogList.GetRandomDog(), pos, targetPack.transform.rotation);
            targetPack.AddDog(dog.GetComponent<DogBase>());
        }
    }

    public void SpawnObject(int[] listOfSpawnIds){
        for (int i = 0; i < listOfSpawnIds.Length; i++){
            GameObject dog = dogList.GetDog(listOfSpawnIds[i]);
            targetPack.AddDog(dog.GetComponent<DogBase>());
        }
    }

    public void GetTargetPack(DogPack target){
        targetPack = target;
    }
}
