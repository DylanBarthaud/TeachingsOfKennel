using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public abstract class DogBase : MonoBehaviour, IHasId, ISpawnsButtons
{
    [SerializeField] protected string dogName; 
    [SerializeField] protected string description;
    [SerializeField] protected int breedId;
    [SerializeField] protected float dogFaith; 
    [SerializeField] protected float dogSpeed;
    [SerializeField] protected int barkStrength;
    [SerializeField] protected float barkSpeed;
    [SerializeField] protected Sprite sprite; 
    protected int packId;

    private UiManager uiManager;

    private ButtonDataStruct buttonData; 
    private DogDataStruct dogData;

    Vector2 movePos;

    private void Awake() {
        buttonData = new ButtonDataStruct() {
            text = dogName,
            sprite = this.sprite,
            eventParent = this,
            transformParent = null
        };

        dogData = new DogDataStruct()
        {
            icon = sprite,
            breed = dogName,
            description = this.description,
            faith = dogFaith,
            barkStrength = this.barkStrength,
            barkSpeed = this.barkSpeed, 
        };

        uiManager = UiManager.instance; 
        movePos = transform.position;
    }

    private void Update(){
        Vector2 newMovePos = new Vector2(movePos.x, movePos.y);
        transform.position = Vector2.MoveTowards(transform.position, newMovePos, dogSpeed * Time.deltaTime);
    }

    public void MoveDogGraphic(Vector2 movePos){
        this.movePos = movePos;
    }

    public IEnumerator StartBark(DogPack dogPack, DogPack target){
        yield return new WaitForSeconds(barkSpeed);
        Bark(dogPack, target);
        dogPack.TickBarks(target);
    }
    // Abstract functions
    public abstract void Bark(DogPack dogPack, DogPack target);

    // Display dogs current stats 
    public void OnButtonClick(GameObject buttonObj){
        uiManager.SetDogStatWindow(dogData);
        uiManager.ActivateWindow(uiManager.dogStats);
    }

    // Getters
    public float GetFaith() { 
        return dogFaith; 
    }

    public float GetSpeed() {
        return dogSpeed; 
    }

    public int GetBreedId(){
        return breedId; 
    }
   
    public int GetId(){
        return packId;
    }

    public Sprite GetSprite(){
        return sprite;
    }

    public ButtonDataStruct GetButtonData() { 
        return buttonData;
    }

    //Setters
    public void SetId(int id)
    {
        packId = id;
    }
}

