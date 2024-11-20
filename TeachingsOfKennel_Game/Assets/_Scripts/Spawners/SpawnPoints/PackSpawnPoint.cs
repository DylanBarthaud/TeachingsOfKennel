using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns Packs on itself after amount of time given 
// Repeats 
public class PackSpawnPoint : MonoBehaviour
{
    [SerializeField] private PacksManager packsManager;
    [SerializeField] private GameObject spawnPoint;

    [SerializeField] private int amountOfPacks; 
    [SerializeField] private int dogCountMin, dogCountMax;
    [SerializeField] private float coolDown;
    [SerializeField] private int MaxAmountOfSpawns;
    private float currentCD; 
    private void Update()
    { 
        currentCD -= Time.deltaTime;

        if (currentCD <= 0 && MaxAmountOfSpawns > 0)
        {
            SpawnPacks(); 
            currentCD = coolDown;
            MaxAmountOfSpawns--; 
        }
    }

    private void SpawnPacks(){
        int[] dogs = new int[amountOfPacks];

        for (int i = 0; i < amountOfPacks; i++){
            dogs[i] = Random.Range(dogCountMin, dogCountMax);
        }

        Vector3 startPos = new Vector3(transform.position.x, transform.position.y, 0);
        packsManager.SpawnPack(packsManager.enemyPackTemplate, amountOfPacks, dogs, new int[] {0}, startPos); 
    }
}

