using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public enum State { freeRoam, fight, cooldown }

public class DogPack : MonoBehaviour, IHasId, ISpawnsButtons
{
    // This class handles an individual dog pack
    // Stores list of dogs in pack
    // Stores stats of dog pack
    // Has functionality for modifying dog pack data

    protected Utilities utilities = new Utilities();
    protected List<DogBase> dogs = new List<DogBase>();

    [SerializeField] protected Slider faithSlider;
    private ButtonDataStruct buttonData; 
    
    protected float packFaith;
    protected float packSpeed = 1;
    protected int packId;

    protected Vector3 flagPos;

    private State state;

    private void Awake(){
        buttonData = new ButtonDataStruct(){
            text = null, 
            sprite = null,
            eventParent = this, 
            transformParent = gameObject.transform 
        };

        UiManager.instance.SpawnButtons(new List<ButtonDataStruct>() { buttonData }, 0, 1, GameObject.Find("Canvas").transform);
        state = State.freeRoam;
        SetMaxFaith();
    }

    // For adding/removing dogs to/from pack
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

    // Battle functionality               
    //
    // "TickBarks"
    // Calls the "bark" function in a DogBase class
    // tickCounter will cycle through all dogs in list then reset 
    //
    // "ConvertRandom"
    // Randomly selects dogs from list
    // Converts selected dogs to new, given, pack

    private int tickCounter = 0; 
    public void TickBarks(DogPack target){
        if (tickCounter >= dogs.Count){  
            tickCounter = 0;
        }

        SetState(State.fight);

        if (GetFaith() <= 0){
            ConvertRandom(target);
            StartCoroutine(FightCoolDown()); 
            return; 
        }

        else if (target.GetFaith() <= 0){
            StartCoroutine(FightCoolDown());
        }

        else{
            StartCoroutine(dogs[tickCounter].Bark(this, target));
            tickCounter++;
        }
    }

    private void ConvertRandom(DogPack newPack){
        if (dogs.Count < 0){
            return; 
        }

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

    private IEnumerator FightCoolDown(){
        SetMaxFaith();
        SetState(State.cooldown);
        yield return new WaitForSeconds(5); 
        SetState(State.freeRoam);
    }

    // Logic for moving pack
    // "MoveFlag" sets target position for pack
    // "MoveAllDogs" passes positions around target position for dogs to move towards
    protected void MoveFlag(Vector2 newFlagPos) {
        if (state == State.fight) {
            flagPos =  transform.position;
        }
        else{
            flagPos = newFlagPos;
        }
        MoveAllDogs(); 
    }

    public void MoveAllDogs(){
        List<Vector3> targetPositions = utilities.GetPosListAround(flagPos, new float[] { 0.25f, 0.5f, 0.75f, 1f }, new int[] { 5, 10, 15, dogs.Count - 31 });

        int targetPositionIndex = 0;
        foreach (DogBase dogBase in dogs){
            dogBase.MoveDogGraphic(targetPositions[targetPositionIndex]);
            targetPositionIndex++;
        }
    }

    // When pack is clicked on 
    // Spawn buttons pertaining to each dog in list
    public void OnButtonClick()
    {
        List<ButtonDataStruct> dogButtons = new List<ButtonDataStruct>();
        for (int i = 0; i < dogs.Count; i++)
        {
            dogButtons.Add(dogs[i].GetButtonData());
        }

        Transform container = UiManager.instance.statContainer.transform;
        UiManager.instance.ClearContainer(container);
        UiManager.instance.SpawnButtons(dogButtons, 1, dogs.Count, container); 
    }

    // Getters
    public int GetId()
    {
        return packId;
    }

    public float GetFaith()
    {
        return packFaith;
    }

    public State GetState()
    {
        return state;
    }

    public ButtonDataStruct GetButtonDataStruct()
    {
        return buttonData;
    }

    //Setters
    public void SetFaith(float x){
        packFaith += x;
        faithSlider.value = packFaith;
    }

    public void SetMaxFaith(){
        float x = 0;
        foreach (DogBase dog in dogs){
            x += dog.GetFaith();
        }

        packFaith = x;
        faithSlider.maxValue = packFaith;
        faithSlider.value = packFaith;
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
}
