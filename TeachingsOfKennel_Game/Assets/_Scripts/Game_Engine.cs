using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {freeRoam, fight} 


public class Game_Engine : MonoBehaviour
{
    public static Game_Engine instance;
    private ISpawner packSpawner; 
    private State state;

    //Temp
    [SerializeField] private Player_DogPack player;
    [SerializeField] private Ai_DogPack enemy;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        packSpawner = GameObject.Find("DogPackSpawner").GetComponent<ISpawner>();
        packSpawner.SpawnObject(this.gameObject, 30);
        packSpawner.SpawnObject(this.gameObject, 10);
        packSpawner.SpawnObject(this.gameObject, 5);
    }

    public void StartDogFight(DogPack attacker, DogPack deffender){
        state = State.fight;

        if (attacker.GetFaith() <= 0)
        {
            attacker.ConvertRandom(deffender);
            state = State.freeRoam;
            return;
        }

        else if (deffender.GetFaith() <= 0)
        { 
            deffender.ConvertRandom(attacker);
            state = State.freeRoam;
            return;
        }

        if (attacker.GetFaith() > 0 && deffender.GetFaith() > 0){
            StartCoroutine(fightTicker(attacker, deffender));
        }
    }

    private IEnumerator fightTicker(DogPack attacker, DogPack deffender) {
        attacker.TickBarks(deffender);
        yield return new WaitForSeconds(1f);
        deffender.TickBarks(attacker);
        yield return new WaitForSeconds(1f);
        StartDogFight(attacker, deffender);
    }

    public Vector2 GetNewPos(int minRange, int maxRange)
    {
        return new Vector2(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + Random.Range(minRange, maxRange));
    }

    public State GetState(){
        return state;
    }
}
