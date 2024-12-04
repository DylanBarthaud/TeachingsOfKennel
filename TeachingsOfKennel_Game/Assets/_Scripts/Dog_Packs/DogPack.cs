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
    private List<DogBase> storedDogs = new List<DogBase>();
    private List<DogBase> activeDogs = new List<DogBase>();
    UiManager uiManager;

    [SerializeField] protected Slider faithSlider;
    private ButtonDataStruct buttonData; 
    
    [SerializeField] protected float packFaith;
    protected float packSpeed = 1;
    protected int packId;

    protected Vector2 flagPos;

    private State state;

    private void Awake(){
        buttonData = new ButtonDataStruct(){
            text = null, 
            sprite = null,
            eventParent = this, 
            transformParent = gameObject.transform 
        };

        uiManager = UiManager.instance;
        uiManager.SpawnButtons(new List<ButtonDataStruct>() { buttonData }, 0, 1, uiManager.packButtonContainer.transform);
        state = State.freeRoam;
        SetMaxFaith();
    }

    // For adding/removing dogs to/from pack
    public void AddDog(DogBase dog){
        dog.SetId(packId);
        if(packId == 0){
            dog.SetScale(new Vector3(1, 1, 1)); 
        }

        activeDogs.Add(dog);
        if (activeDogs.Count > 15){
           DeactivateDog(dog);
        }

        if(state != State.fight) {
            SetMaxFaith();
        }
    }

    private void RemoveDog(DogBase dog){ 
        activeDogs.Remove(dog);
            
        if (activeDogs.Count <= 0){ 
            Destroy(gameObject);
        }

        if (state != State.fight){
            SetMaxFaith();
        };
    }

    public void ActivateDog(DogBase dog){
        storedDogs.Remove(dog);
        activeDogs.Add(dog);
        dog.gameObject.SetActive(true);
        dog.transform.position = new Vector2(transform.position.x + 5, transform.position.y); 
        MoveAllDogs();
        SetMaxFaith();
    }

    public void DeactivateDog(DogBase dog){
        activeDogs.Remove(dog);
        storedDogs.Add(dog);
        dog.gameObject.SetActive(false);
        SetMaxFaith();
    }

    public void SwapDogs(int dog1, int dog2, List<DogBase> list){
        DogBase temp = list[dog2];
        list[dog2] = list[dog1];
        list[dog1] = temp;
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
        if (tickCounter >= activeDogs.Count){  
            tickCounter = 0;
            ResetDogStats();
        }

        SetState(State.fight);

        if (GetFaith() <= 0){
            ConvertRandom(target);
            StartCoroutine(FightCoolDown());
            ResetDogStats();
            tickCounter = 0;
        }

        else if (target.GetFaith() <= 0){
            StartCoroutine(FightCoolDown());
            ResetDogStats();
            tickCounter = 0;
        }

        else{
            StartCoroutine(activeDogs[tickCounter].StartBark(this, target));
            tickCounter++;
        }
    }

    private void ResetDogStats(){
        foreach (DogBase dog in activeDogs) {
            dog.ResetStatsToBase(); 
        }
    }

    private void ConvertRandom(DogPack newPack){
        if (activeDogs.Count < 0){
            return; 
        }

        List<DogBase> tempDogList = new List<DogBase>();

        foreach (DogBase dog in activeDogs){
            int x = Random.Range(1, 101);
            if(x >= dog.GetBaseFaith() || activeDogs.Count <= 1){
                tempDogList.Add(dog);
            }
        }

        for (int i = 0; i < tempDogList.Count; i++){
            ConvertDog(tempDogList[i], newPack);
        }
    }

    public void ConvertDog(DogBase dog, DogPack newPack){
        RemoveDog(dog);
        newPack.AddDog(dog);

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
        flagPos = transform.position;

        if (state != State.fight) {
            int layerMask = LayerMask.GetMask("Walls");
            Vector2 direction = (newFlagPos - flagPos).normalized;

            RaycastHit2D hit = Physics2D.Raycast(
                flagPos,
                direction,
                Vector2.Distance(flagPos, newFlagPos),
                layerMask
            );

            flagPos = hit.collider != null 
                ? hit.point - direction * 0.5f 
                : newFlagPos;
        }

        MoveAllDogs(); 
    }

    public void MoveAllDogs(){
        List<Vector3> targetPositions = utilities.GetPosListAround(flagPos, 
            new float[] { 0.3f, 0.6f, 0.9f, 1.2f },
            new int[] { 5, 10, 15, activeDogs.Count - 31 }
            );

        int targetPositionIndex = 0;
        foreach (DogBase dogBase in activeDogs){
            dogBase.MoveDogGraphic(targetPositions[targetPositionIndex]);
            targetPositionIndex++;
        }
    }

    // When pack is clicked on 
    // Spawn buttons pertaining to each dog in list
    public void OnButtonClick(GameObject buttonObj)
    {
        List<ButtonDataStruct> dogButtons = new List<ButtonDataStruct>();
        for (int i = 0; i < activeDogs.Count; i++){
            dogButtons.Add(activeDogs[i].GetButtonData());
        }

        Transform container = uiManager.statContainer.transform;
        GameObject packViewer = uiManager.packViewer;
        uiManager.ClearContainer(container);
        uiManager.ActivateWindow(packViewer);
        uiManager.SpawnButtons(dogButtons, 1, activeDogs.Count, container); 
    }

    // Getters
    public int GetId(){
        return packId;
    }

    public float GetFaith(){
        return packFaith;
    }

    public int GetDogindex(){
        return tickCounter;
    }

    public State GetState(){
        return state;
    }

    public ButtonDataStruct GetButtonDataStruct(){
        return buttonData;
    }

    public List<DogBase> GetActiveDogs(){
        return activeDogs;
    }

    public List<DogBase> GetStoredDogs(){
        return storedDogs;
    }

    //Setters
    public void SetFaith(float x){
        packFaith += x;
        faithSlider.value = packFaith;
    }

    public void SetMaxFaith(){
        float x = 0;
        foreach (DogBase dog in activeDogs){
            x += dog.GetBaseFaith();
        }

        packFaith = x;
        faithSlider.maxValue = packFaith;
        faithSlider.value = packFaith;
    }

    public void SetPos(){
        if (activeDogs.Count == 0){
            return; 
        }

        transform.position = activeDogs[0].transform.position; 
    }

    private bool idSet = false; 
    public void SetId(int id){
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
