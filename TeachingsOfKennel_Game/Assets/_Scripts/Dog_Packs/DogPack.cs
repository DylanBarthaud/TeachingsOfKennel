using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPack : MonoBehaviour
{
    [SerializeField] protected List<DogBase> dogs = new List<DogBase>();
    protected float packFaith = 100;
    protected float packSpeed = 1;

    protected List<Dog_Graphic> dog_Graphics = new List<Dog_Graphic>();
    [SerializeField] private Dog_Graphic_Handler graphic_Handler;
    [SerializeField] private string pack_Tag; 

    protected Vector3 flagPos;

    private void Start(){
        GlobalEventSystem.instance.onPackDetection += startBattle; 
    }

    public void AddDog(DogBase dog){
        dogs.Add(dog);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 1);
        Dog_Graphic dog_Graphic = Instantiate(dog.GetGraphic(), pos, transform.rotation);
        dog_Graphic.SetId(dog.GetId()); 
        dog_Graphic.tag = pack_Tag;
        dog_Graphics.Add(dog_Graphic); 
    }

    private void RemoveDog(DogBase dog){ 
        dogs.Remove(dog);
        dog_Graphics.Remove(dog.GetGraphic());

        if (dogs.Count >= 0) { 
            gameObject.SetActive(false);
        }
    }

    public void TickBarks(DogPack target){
        foreach (DogBase dog in dogs) {
            dog.bark(target); 
        }
    }

    public void startBattle(string tag,DogPack attacker) {
        if (pack_Tag == tag){
            Game_Engine.instance.StartDogFight(attacker, this);
        }
    }

    public void ConvertRandom(DogPack newPack){
        if (dogs.Count < 0){
            return; 
        }

        List<DogBase> tempDogList = new List<DogBase>();

        foreach (DogBase dog in dogs){
            tempDogList.Add(dog); 
        }

        print(tempDogList.Count);

        for (int i = 0; i < tempDogList.Count; i++){
            for (int j = 0; j < dog_Graphics.Count; j++){
                //print(tempDogList[i].GetId() + " " + dog_Graphics[j].GetId());
                if (tempDogList[i].GetId() == dog_Graphics[j].GetId()){
                    RemoveDog(tempDogList[i]);
                    newPack.Convert(tempDogList[i], dog_Graphics[j]); 
                }
            }
        }
    }

    public void Convert(DogBase dog, Dog_Graphic graphic){
        dogs.Add(dog);
        graphic.tag = pack_Tag;
        dog_Graphics.Add(graphic); 
    }

    protected void MoveFlag(Vector3 newFlagPos) {
        flagPos = newFlagPos;
        MoveGraphics(); 
    }

    public void MoveGraphics(){
        graphic_Handler.MoveAllDogGraphics(dog_Graphics, flagPos, packSpeed); 
    }

    protected void AssignGraphics(){
        dog_Graphics = new List<Dog_Graphic>();
        foreach (DogBase dog in dogs){
            dog_Graphics.Add(dog.GetGraphic());
        }
    }

    public float GetFaith() { 
        return packFaith;
    }

    public void SetFaith(float x){
        packFaith += x;
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

    public void SetPos(){
        transform.position = dog_Graphics[0].transform.position; 
    }
}
