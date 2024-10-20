using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {freeRoam, fight} 


public class Game_Engine : MonoBehaviour
{
    public static Game_Engine instance;
    [SerializeField] private Player_DogPack player;
    [SerializeField] private Ai_DogPack enemy;
    private State state;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        SpawnDogs(player, 5); 
        SpawnDogs(enemy, 3);

    }

    public void SpawnDogs(DogPack targetPack, int numOfDogs)
    {
        for (int i = 0; i < numOfDogs; i++)
        {
            GameObject dog = GlobalDogList.instance.GetRandomDog();
            targetPack.AddDog(dog.GetComponent<DogBase>());
        }
    }

    public void SpawnDogs(DogPack targetPack, int[] dogIds)
    {
        for(int i = 0; i < dogIds.Length; i++)
        {
            GameObject dog = GlobalDogList.instance.GetDog(dogIds[i]); 
            targetPack.AddDog(dog.GetComponent<DogBase>());
        }
    }

    public void StartDogFight(DogPack attacker, DogPack deffender){
        print("start battle");
        state = State.fight;
        print(attacker.GetFaith() + " " + deffender.GetFaith()); 
        if (attacker.GetFaith() > 0 && deffender.GetFaith() > 0){
            StartCoroutine(fightTicker(attacker, deffender));
            return; 
        }

        if (attacker.GetFaith() <= 0){
            print("attacker lost"); 
            attacker.ConvertRandom(deffender);
        }

        else { print("deffender lost");  deffender.ConvertRandom(attacker); }
    }

    private IEnumerator fightTicker(DogPack attacker, DogPack deffender) {
        attacker.TickBarks(deffender);
        yield return new WaitForSeconds(3);
        deffender.TickBarks(attacker);
        yield return new WaitForSeconds(2);
        StartDogFight(attacker, deffender);
    }

    public State GetState()
    {
        return state;
    }
}
