using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacksManager : MonoBehaviour
{
    // Spawns packs
    // Keeps track of amount of packs that have been spawned and gradually ramps up difficulty 

    private Utilities utilities = new Utilities();
    [SerializeField] SpawnGameObjects spawner;
    [SerializeField] private DogList dogList;
    [SerializeField] public GameObject playerPackTemplate;
    [SerializeField] public GameObject enemyPackTemplate;

    // Spawns the amount of packs specified
    // At location given
    private int numOfPacks = 0; 
    public void SpawnPack(GameObject pack, int amount, int[] amountOfDogs, Vector3 startLocation){
        List<Vector3> targetSpawnPositions = utilities.GetPosListAround(startLocation, 3, amount);
        GameObject[] packObjs = spawner.SpawnGameObject(pack, amount, targetSpawnPositions, transform.rotation);

        int i = 0; 
        foreach (GameObject packObj in packObjs){
            DogPack currentPack = packObj.GetComponent<DogPack>();
            currentPack.SetId(numOfPacks);
            SpawnDogs(amountOfDogs[i], currentPack); 
            numOfPacks++;
            i++; 
        }
    }

    // Spawns amount of dogs in a pack
    // Adds dogs to packs list 
    private void SpawnDogs(int amount, DogPack pack){
        Vector2 pos = new Vector3(pack.transform.position.x, pack.transform.position.y);

        for (int i = 0; i < amount; i++){    
            GameObject dogObj = Instantiate(dogList.GetRandomDog(), pos, pack.transform.rotation);
            pack.AddDog(dogObj.GetComponent<DogBase>());
        }
    }

    public int GetNumberOfPacks(){   
        return numOfPacks;
    }
}