using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBase : MonoBehaviour, IHasId
{
    [SerializeField] protected int breedId;
    protected int packId; 

    [SerializeField] protected float dogFaith; 
    [SerializeField] protected float dogSpeed;
    [SerializeField] protected int barkStrength;

    Vector2 movePos;

    private void Awake(){
        movePos = transform.position;
    }

    private void Update(){
        Vector2 newMovePos = new Vector2(movePos.x, movePos.y);
        transform.position = Vector2.MoveTowards(transform.position, newMovePos, dogSpeed * Time.deltaTime);
    }

    public void MoveDogGraphic(Vector2 movePos){
        this.movePos = movePos;
    }

    public virtual void Bark(DogPack target) {
        target.SetFaith(-barkStrength);
    }

    public float GetFaith() { 
        return dogFaith; 
    }

    public float GetSpeed() {
        return dogSpeed; 
    }

    public Vector2 GetPos(){
        return transform.position;
    }

    public int GetBreedId(){
        return breedId; 
    }

    public void SetId(int id){
        packId = id;
    }
   
    public int GetId(){
        return packId;
    }
}

