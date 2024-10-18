using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {freeRoam, fight} 


public class Game_Engine : MonoBehaviour
{
    public static Game_Engine instance;
    [SerializeField] private Player_DogPack player;
    [SerializeField] private Ai_DogPack enemy;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }


        GameObject dog = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic = Instantiate(dog.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog.GetComponent<DogBase>());

        GameObject dog2 = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic2 = Instantiate(dog2.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog2.GetComponent<DogBase>());

        GameObject dog3 = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic3 = Instantiate(dog3.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog3.GetComponent<DogBase>());


        GameObject enemy_dog = GlobalDogList.instance.GetDog(1);
        Dog_Graphic enemy_Graph = Instantiate(enemy_dog.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        enemy.AddDog(enemy_dog.GetComponent<DogBase>());

    }

    private State state;
    public void StartDogFight(DogPack attacker, DogPack deffender){
        print("start battle");
        state = State.fight;
        print(attacker.GetFaith() + " " + deffender.GetFaith()); 
        if (attacker.GetFaith() !>= 0 && deffender.GetFaith() !>= 0){
            StartCoroutine(fightTicker(attacker, deffender)); 
        }

        if (attacker.GetFaith() <= 0){
            print("attacker lost"); 
            attacker.ConvertRandom(deffender);
        }

        else { print("deffender lost");  deffender.ConvertRandom(attacker); }
    }

    private IEnumerator fightTicker(DogPack attacker, DogPack deffender) {
        attacker.TickBarks(deffender);
        yield return new WaitForSeconds(1);
        deffender.TickBarks(attacker);
        yield return new WaitForSeconds(1);
        StartDogFight(attacker, deffender);
    }
}
