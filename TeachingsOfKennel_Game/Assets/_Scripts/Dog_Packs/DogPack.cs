using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public enum State { freeRoam, fight, cooldown }

public class DogPack : MonoBehaviour, IHasId, ISpawnsButtons
{
    protected Utilities utilities = new Utilities();
    private ButtonDataStruct buttonData; 

    protected List<DogBase> dogs = new List<DogBase>();
    protected float packFaith;
    [SerializeField] protected Slider faithSlider; 
    protected float packSpeed = 1;
    private State state;
    protected int packId;

    protected Vector3 flagPos;

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

    public void ConvertRandom(DogPack newPack){
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

    public ButtonDataStruct GetButtonDataStruct(){
        return buttonData;
    }
}
