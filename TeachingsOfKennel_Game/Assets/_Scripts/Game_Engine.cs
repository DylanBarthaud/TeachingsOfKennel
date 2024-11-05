using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Engine : MonoBehaviour
{
    private Utilities utilities = new Utilities(); 

    public static Game_Engine instance;
    private PackSpawner packSpawner; 
    
    private void Awake(){
        if(instance == null){
            instance = this;
        }

        packSpawner = GameObject.Find("DogPackSpawner").GetComponent<PackSpawner>();
        packSpawner.SpawnObject(1, 10);
        packSpawner.SpawnObject(3, 2, 8);
    }

    public void StartDogFight(DogPack attacker, DogPack deffender){

        if (attacker.GetFaith() <= 0){
            attacker.ConvertRandom(deffender);
            attacker.SetMaxFaith();
            deffender.SetMaxFaith(); 
            StartCoroutine(FightCooldown(attacker, deffender)); 
            return;
        }

        else if (deffender.GetFaith() <= 0){ 
            deffender.ConvertRandom(attacker);
            attacker.SetMaxFaith();
            deffender.SetMaxFaith();
            StartCoroutine(FightCooldown(attacker, deffender));
            return;
        }

        if (attacker.GetFaith() > 0 && deffender.GetFaith() > 0){
            StartCoroutine(FightTicker(attacker, deffender));
        }
    }

    private IEnumerator FightTicker(DogPack attacker, DogPack deffender) {
        attacker.TickBarks(deffender);
        yield return new WaitForSeconds(1f);
        deffender.TickBarks(attacker);
        yield return new WaitForSeconds(1f);
        StartDogFight(attacker, deffender);
    }

    private IEnumerator FightCooldown(DogPack attacker, DogPack deffender) {
        attacker.SetState(State.cooldown);
        deffender.SetState(State.cooldown);
        yield return new WaitForSeconds(10f);
        attacker.SetState(State.freeRoam);
        deffender.SetState(State.freeRoam);
    }
}
