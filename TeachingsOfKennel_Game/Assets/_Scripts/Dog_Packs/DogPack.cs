using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPack : MonoBehaviour
{
    protected List<DogBase> dogs = new List<DogBase>();
    protected float packFaith = 100;
    protected float packSpeed = 2;

    protected List<Dog_Graphic> dog_Graphics = new List<Dog_Graphic>();
    [SerializeField] private Dog_Graphic_Handler graphic_Handler;
    [SerializeField] private string pack_Tag; 

    protected Vector3 flagPos;

    private void Start()
    {
        GlobalEventSystem.instance.onPackDetection += startBattle; 
    }

    public void AddDog(DogBase dog){
        dogs.Add(dog);
        dog.GetGraphic().tag = pack_Tag;
        dog_Graphics.Add(dog.GetGraphic()); 
    }

    private void RemoveDog(DogBase dog){ 
        dogs.Remove(dog);
        dog_Graphics.Remove(dog.GetGraphic());
    }

    public void TickBarks(DogPack target){
        foreach (DogBase dog in dogs) {
            dog.bark(target); 
        }
    }

    public void startBattle(string tag,DogPack attacker) {
        if (pack_Tag == tag){
            print("start");
            Game_Engine.instance.StartDogFight(attacker, this);
        }
    }

    public void ConvertRandom(DogPack newPack){
        if (dogs.Count >= 0)
        {
            List<DogBase> tempDogList = new List<DogBase>();
            foreach (DogBase dog in dogs)   
            {
                int x = Random.Range(1, 101);
                if (x >= dog.GetFaith())
                {
                    tempDogList.Add(dog);
                }
            }

            foreach (DogBase dog in tempDogList)
            {
                AddDog(dog); 
                RemoveDog(dog);
            }
        }
        else { gameObject.SetActive(false); }
    }

    protected void MoveFlag(Vector3 newFlagPos) {
        flagPos = newFlagPos;
        MoveGraphics(); 
    }

    public void MoveGraphics()
    {
        graphic_Handler.MoveAllDogGraphics(dog_Graphics, flagPos, packSpeed); 
    }

    protected void AssignGraphics()
    {
        dog_Graphics = new List<Dog_Graphic>();
        foreach (DogBase dog in dogs)
        {
            dog_Graphics.Add(dog.GetGraphic());
        }
    }

    public float GetFaith() { 
        return packFaith;
    }

    public void SetFaith(float x){
        packFaith = x;
    }

    private void SetMaxFaith() {
        float x = 0;
        foreach (DogBase dog in dogs) { 
            x += dog.GetFaith();
        }
        packFaith = x;
    }

    private void SetSpeed() {
        float x = 0;
        foreach (DogBase dog in dogs) { 
            x += dog.GetSpeed();
        }
        packSpeed = x / dogs.Count;
    }

    public void SetPos()
    {
        transform.position = dog_Graphics[0].transform.position; 
    }
}
