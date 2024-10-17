using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {freeRoam, fight} 


public class Game_Engine : MonoBehaviour
{
    [SerializeField] private Player_DogPack player;

    private void Awake()
    {
        GameObject dog = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic = Instantiate(dog.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog.GetComponent<DogBase>(), graphic);

        GameObject dog2 = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic2 = Instantiate(dog2.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog2.GetComponent<DogBase>(), graphic2);

        GameObject dog3 = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic3 = Instantiate(dog3.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog3.GetComponent<DogBase>(), graphic3);

        GameObject dog4 = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic4 = Instantiate(dog4.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog4.GetComponent<DogBase>(), graphic4);

        GameObject dog5 = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic5 = Instantiate(dog5.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog5.GetComponent<DogBase>(), graphic5);

        GameObject dog6 = GlobalDogList.instance.GetDog(1);
        Dog_Graphic graphic6 = Instantiate(dog6.gameObject.GetComponent<DogBase>().GetGraphic(), transform);
        player.AddDog(dog6.GetComponent<DogBase>(), graphic6);
    }

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
