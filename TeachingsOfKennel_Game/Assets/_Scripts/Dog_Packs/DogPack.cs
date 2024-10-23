using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPack : MonoBehaviour, IHasId
{
    [SerializeField] protected List<DogBase> dogs = new List<DogBase>();
    [SerializeField] protected float packFaith;
    protected float packSpeed = 1;

    private Dog_Movement_Handler movement_Handler; 
    [SerializeField] private int packId; 

    protected Vector3 flagPos;

    private void Awake(){
        movement_Handler = GameObject.Find("Dog_Movement_Handler").GetComponent<Dog_Movement_Handler>(); 

        GlobalEventSystem.instance.onPackDetection += startBattle;
    }

    private void Start()
    {
        SetMaxFaith();
    }

    public void AddDog(DogBase dog){

        dogs.Add(dog);
        dog.SetId(packId);
    }

    private void RemoveDog(DogBase dog){ 
        dogs.Remove(dog);

        if (dogs.Count <= 0) { 
            gameObject.SetActive(false);
        }
    }

    public void TickBarks(DogPack target){
        foreach (DogBase dog in dogs) {
            dog.Bark(target); 
        }
    }

    public void startBattle(int packId,DogPack attacker) {
        if (this.packId == packId){
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

        for (int i = 0; i < tempDogList.Count; i++){
            RemoveDog(tempDogList[i]); 
            newPack.AddDog(tempDogList[i]);
        }
    }

    protected void MoveFlag(Vector3 newFlagPos) {
        flagPos = newFlagPos;
        MoveGraphics(); 
    }

    public void MoveGraphics(){
        movement_Handler.MoveAllDogGraphics(dogs, flagPos, packSpeed); 
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
            print(dog.GetFaith()); 
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
        if (dogs.Count == 0) {
            return; 
        }
        transform.position = dogs[0].transform.position; 
    }

    private bool idSet = false; 
    public void SetId(int id)
    {
        if (!idSet)
        {
            packId = id;
            idSet = true;
        }

        else { Debug.LogError("PackTag has already been set as: " + packId); }
    }

    public int GetId()
    {
        return packId;
    }
}
