using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public abstract class DogBase : MonoBehaviour, IHasId, ISpawnsButtons
{
    [SerializeField] protected string dogName; 
    [SerializeField] protected int breedId;
    [SerializeField] protected float dogFaith; 
    [SerializeField] protected float dogSpeed;
    [SerializeField] protected int barkStrength;
    [SerializeField] protected float barkSpeed;
    protected int packId;

    private ButtonDataStruct buttonData; 

    Vector2 movePos;

    private void Awake() {
        buttonData = new ButtonDataStruct() {
            text = dogName,
            sprite = null, 
            eventParent = this, 
            transformParent = null
        };

        movePos = transform.position;
    }

    private void Update(){
        Vector2 newMovePos = new Vector2(movePos.x, movePos.y);
        transform.position = Vector2.MoveTowards(transform.position, newMovePos, dogSpeed * Time.deltaTime);
    }

    public void MoveDogGraphic(Vector2 movePos){
        this.movePos = movePos;
    }

    // Abstract functions
    public abstract IEnumerator Bark(DogPack dogPack, DogPack target);

    // Display dogs current stats 
    public void OnButtonClick()
    {
        print("Name: " + dogName);
        print("Faith: " + dogFaith);
        print("Speed: " + dogSpeed);
        print("Bark Strength: " + barkStrength);
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

    public ButtonDataStruct GetButtonData() { 
        return buttonData;
    }

    //Setters
    public void SetId(int id)
    {
        packId = id;
    }
}

