using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {freeRoam, fight} 


public class Game_Engine : MonoBehaviour
{
    private State state;
    public void StartDogFight(DogPack attacker, DogPack deffender){

        state = State.fight; 

        if (attacker.GetFaith() != 0 && deffender.GetFaith() != 0){
            attacker.TickBarks(deffender);
            StartCoroutine(fightTicker());
            deffender.TickBarks(attacker);
            StartCoroutine(fightTicker()); 
            StartDogFight(attacker, deffender);
        }

        if (attacker.GetFaith() <= 0){
            attacker.ConvertRandom(deffender);
        }

        else { deffender.ConvertRandom(attacker); }
    }

    private IEnumerator fightTicker() {
        yield return new WaitForSeconds(1);
    }
}
