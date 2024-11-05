using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackSpawner : MonoBehaviour
{
    private Utilities utilities = new Utilities(); 

    [SerializeField] private DogSpawner dogSpawner;
    [SerializeField] private GameObject packTemplate;
    [SerializeField] private GameObject playerPackTemplate;
    [SerializeField] private float range; 
    private int numOfPacks = 0;

    //  SpawnObject (|Number of packs to spawn| |Number of dogs to spawn in pack|)
    public void SpawnObject(int numberToSpawn, int numberOfItems ){
        DogPack newPack;

        if (numOfPacks == 0){
            newPack = SpawnPlayer();
            dogSpawner.GetTargetPack(newPack);
            dogSpawner.SpawnObject(numberOfItems);
        }
        else{
            List<Vector3> targetSpawnPositions = utilities.GetPosListAround(transform.position, range, numberToSpawn);
            int targetPosIndex = 0; 
            for (int i = 0; i < numberToSpawn; i++){
                newPack = SpawnPack(targetSpawnPositions[targetPosIndex]);
                targetPosIndex++; 
                dogSpawner.GetTargetPack(newPack);
                dogSpawner.SpawnObject(numberOfItems);
            }
        }
    }

    public void SpawnObject(int numberToSpawn, int[] listOfItemIds){
        DogPack newPack;

        if (numOfPacks == 0){
            newPack = SpawnPlayer();
            dogSpawner.GetTargetPack(newPack);
            dogSpawner.SpawnObject(listOfItemIds);
        }
        else{
            List<Vector3> targetSpawnPositions = utilities.GetPosListAround(transform.position, range, numberToSpawn);
            int targetPosIndex = 0;
            for (int i = 0; i < numberToSpawn; i++){
                newPack = SpawnPack(targetSpawnPositions[targetPosIndex]);
                targetPosIndex++;
                dogSpawner.GetTargetPack(newPack);
                dogSpawner.SpawnObject(listOfItemIds);
            }
        }
    }

    public void SpawnObject(int numberToSpawn, int randMin, int randMax){
        DogPack newPack;

        if (numOfPacks == 0){
            newPack = SpawnPlayer();
            dogSpawner.GetTargetPack(newPack);
            dogSpawner.SpawnObject(Random.Range(randMin, randMax));
        }
        else{
            List<Vector3> targetSpawnPositions = utilities.GetPosListAround(transform.position, range, numberToSpawn);
            int targetPosIndex = 0;
            for (int i = 0; i < numberToSpawn; i++){
                newPack = SpawnPack(targetSpawnPositions[targetPosIndex]);
                targetPosIndex++;
                dogSpawner.GetTargetPack(newPack);
                dogSpawner.SpawnObject(Random.Range(randMin, randMax));
            }
        }
    }

    private DogPack SpawnPack(Vector3 pos){
        DogPack newPack = Instantiate(packTemplate.GetComponent<DogPack>(), pos, transform.rotation);
        newPack.SetId(numOfPacks);
        numOfPacks++;
        return newPack;
    }

    private DogPack SpawnPlayer(){
        Vector2 pos = new Vector2(0, 0);
        DogPack newPack = Instantiate(playerPackTemplate.GetComponent<DogPack>(), pos, transform.rotation);
        newPack.SetId(numOfPacks);
        numOfPacks++;
        return newPack;
    }
}
