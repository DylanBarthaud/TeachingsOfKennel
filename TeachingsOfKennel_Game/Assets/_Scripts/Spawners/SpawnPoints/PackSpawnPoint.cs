using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns Packs on itself after amount of time given 
// Repeats 
public class PackSpawnPoint : MonoBehaviour
{
    [SerializeField] private PacksManager packsManager;
    [SerializeField] private int amountOfPacks;
    [SerializeField] private float roamDistance; 
    [SerializeField] private int dogCountMin, dogCountMax;
    [SerializeField] private int[] dogIds;
    [SerializeField] private float coolDown;
    [SerializeField] private int MaxAmountOfSpawns;
    [SerializeField] private float activeRange; 
    private float currentCD; 

    [SerializeField] private bool active; 
    private void Update()
    {
        Collider2D collision = Physics2D.OverlapCircle(this.transform.position, activeRange);

        if (collision != null && collision.gameObject.GetComponent<DogPack>() != null){
            DogPack detectedPack = collision.gameObject.GetComponent<DogPack>();
            if (detectedPack.GetId() == 0){
                active = true; 
            }
        }

        if (!active){
            return;
        }

        currentCD -= Time.deltaTime;
        if (currentCD <= 0 && MaxAmountOfSpawns > 0)
        {
            SpawnPacks(); 
            currentCD = coolDown;
            MaxAmountOfSpawns--; 
        }

        if(MaxAmountOfSpawns <= 0){
            Destroy(gameObject); 
        }
    } 

    private void SpawnPacks(){
        int[] dogs = new int[amountOfPacks];

        for (int i = 0; i < amountOfPacks; i++){
            dogs[i] = Random.Range(dogCountMin, dogCountMax);
        }

        if (dogIds.Length <= 0){
            dogIds = new int[] { 0 }; 
        }

        Vector3 startPos = new Vector3(transform.position.x, transform.position.y, 0);

        DogPack[] packs = new DogPack[amountOfPacks];

        packs = packsManager.SpawnPack(packsManager.enemyPackTemplate, 
            amountOfPacks,
            dogs, 
            dogIds, 
            startPos
            );

        for (int i = 0; i < amountOfPacks; i++){
            packs[i].gameObject.GetComponent<Ai_DogPack>().SetRoamDistance(transform.position, roamDistance);
        }
    }
}

