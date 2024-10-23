using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBase : MonoBehaviour, IHasId
{
    [SerializeField] protected int breedId;
    [SerializeField] protected int packId; 

    protected float dogFaith = 10f; 
    protected float dogSpeed = 1;

    protected int barkStrength = 1;
    protected float barkCoolDown;
    protected float barkCurrentCoolDown;

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
        print(dogFaith); 
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
    public void SetId(int id)
    {
        packId = id;
    }
    public int GetId()
    {
        return packId;
    }
}

