using System; 
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
    public DogPack[] SpawnPack(GameObject pack, int amount, int[] amountOfDogs, int[] dogIds, Vector3 startLocation){
        DogPack[] returnPacks = new DogPack[amount];

        List<Vector3> targetSpawnPositions = utilities.GetPosListAround(startLocation, 1, amount);
        GameObject[] packObjs = spawner.SpawnGameObject(pack, amount, targetSpawnPositions, transform.rotation);

        int i = 0; 
        foreach (GameObject packObj in packObjs){
            DogPack currentPack = packObj.GetComponent<DogPack>();
            currentPack.SetId(numOfPacks); 
            SpawnDogs(amountOfDogs[i], currentPack, dogIds);
            returnPacks[i] = currentPack; 
            numOfPacks++;
            i++; 
        }

        return returnPacks; 
    }

    // Spawns amount of dogs in a pack
    // Adds dogs to packs list 
    private void SpawnDogs(int amount, DogPack pack, int[] ids){
        Vector2 pos = new Vector3(pack.transform.position.x, pack.transform.position.y);

        int index = 0; 
        for (int i = 0; i < amount; i++){

            if (index >= ids.Length){
                index = 0;
            }

            GameObject dogObj = ids[0] == 0
                ? Instantiate(dogList.GetRandomDog(0, dogList.GetListLength() - 1), pos, pack.transform.rotation)
                : Instantiate(dogList.GetDog(ids[index]), pos, pack.transform.rotation);

            pack.AddDog(dogObj.GetComponent<DogBase>());
            index++;
        }
    }

    public int GetNumberOfPacks(){   
        return numOfPacks;
    }
}
