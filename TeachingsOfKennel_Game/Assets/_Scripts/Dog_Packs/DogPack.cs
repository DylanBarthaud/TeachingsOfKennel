using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public enum State { freeRoam, fight, cooldown }

public class DogPack : MonoBehaviour, IHasId, ISpawnsButtons
{
    protected Utilities utilities = new Utilities(); 

    [SerializeField] protected List<DogBase> dogs = new List<DogBase>();
    [SerializeField] protected float packFaith;
    [SerializeField] protected Slider faithSlider; 
    protected float packSpeed = 1;
    [SerializeField] private State state;
    [SerializeField] private int packId; 

    protected DogStatButtons statButtons;
    protected ButtonSpawner buttonSpawner;
    protected Vector3 flagPos;

    private void Awake(){
        GlobalEventSystem.instance.onPackDetection += startBattle;
        statButtons = GameObject.Find("StatButtonSpawn").GetComponent<DogStatButtons>();
        buttonSpawner = GameObject.Find("ButtonSpawner").GetComponent<ButtonSpawner>();
        buttonSpawner.SpawnButton(this, 1, 0, GameObject.Find("Canvas").transform, this.gameObject);

        state = State.freeRoam;
        SetMaxFaith();
    }

    public void AddDog(DogBase dog){
        dogs.Add(dog);
        dog.SetId(packId);
        SetMaxFaith(); 
    }

    private void RemoveDog(DogBase dog){ 
        dogs.Remove(dog);
            
        if (dogs.Count <= 0){ 
            Destroy(gameObject);
        }
        SetMaxFaith();
    }

    public void TickBarks(DogPack target){
        foreach (DogBase dog in dogs){
            dog.Bark(target); 
        }
    }

    public void startBattle(int packId,DogPack attacker) {
        if (this.packId == packId && packId < attacker.GetId()){
            Game_Engine.instance.StartDogFight(attacker, this);
            attacker.SetState(State.fight);
            SetState(State.fight);
        }
    }

    public void ConvertRandom(DogPack newPack){
        if (dogs.Count < 0){
            return; 
        }

        SetMaxFaith();
        List<DogBase> tempDogList = new List<DogBase>();

        foreach (DogBase dog in dogs){
            int x = Random.Range(1, 21);
            if(x >= dog.GetFaith() || dogs.Count <= 1){
                tempDogList.Add(dog);
            }
        }

        for (int i = 0; i < tempDogList.Count; i++){
            RemoveDog(tempDogList[i]); 
            newPack.AddDog(tempDogList[i]);
        }
    }

    protected void MoveFlag(Vector2 newFlagPos) {
        if (state == State.fight) {
            flagPos =  transform.position;
        }
        else{
            flagPos = newFlagPos;
        }
        MoveGraphics(); 
    }

    public void MoveGraphics(){
        MoveAllDogGraphics(); 
    }

    public void MoveAllDogGraphics(){
        List<Vector3> targetPositions = utilities.GetPosListAround(flagPos, new float[] { 0.25f, 0.5f, 0.75f, 1f }, new int[] { 5, 10, 15, dogs.Count - 31 });

        int targetPositionIndex = 0;
        foreach (DogBase dogBase in dogs){
            dogBase.MoveDogGraphic(targetPositions[targetPositionIndex]);
            targetPositionIndex++;
        }
    }

    public void SpawnDogStats(){
        List<ISpawnsButtons> dogButtons = new List<ISpawnsButtons>();
        for (int i = 0; i < dogs.Count; i++) {
            dogButtons.Add(dogs[i].gameObject.GetComponent<ISpawnsButtons>());
        }

        statButtons.SpawnStatButtons(dogButtons); 
    }

    public void SetFaith(float x){
        packFaith += x;
        faithSlider.value = packFaith;
    }

    private void SetMaxFaith(){
        float x = 0;
        foreach (DogBase dog in dogs){
            x += dog.GetFaith();
        }

        packFaith = x;
        faithSlider.maxValue = packFaith;
        faithSlider.value = packFaith;
    }

    private void SetSpeed(){
        float x = 0;
        foreach (DogBase dog in dogs){ 
            x += dog.GetSpeed();
        }

        packSpeed = x / dogs.Count;
    }

    public void SetPos(){
        if (dogs.Count == 0){
            return; 
        }

        transform.position = dogs[0].transform.position; 
    }

    private bool idSet = false; 
    public void SetId(int id)
    {
        if (!idSet){
            packId = id;
            idSet = true;
        }

        else { Debug.LogError("PackTag has already been set as: " + packId); }
    }

    public void SetState(State newState){
        state = newState;
    }

    public int GetId(){
        return packId;
    }

    public float GetFaith(){
        return packFaith;
    }

    public State GetState(){
        return state;
    }

    public void OnButtonClick()
    {
        SpawnDogStats(); 
    }
}
