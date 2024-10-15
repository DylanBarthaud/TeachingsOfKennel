using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPack : MonoBehaviour
{
    protected List<DogBase> dogs;
    protected float packFaith;
    protected float packSpeed; 

    public void AddDog(DogBase dog){
        dogs.Add(dog);
    }

    private void RemoveDog(DogBase dog){ 
        dogs.Remove(dog);
    }

    public void TickBarks(DogPack target){
        foreach (DogBase dog in dogs) {
            dog.bark(); 
        }
    }

    public void ConvertRandom(DogPack newPack){
        foreach (DogBase dog in dogs)
        {
            int x = Random.Range(1, 101);
            if (x >= dog.GetFaith()) {
                newPack.AddDog(dog);
                RemoveDog(dog);
            }

        }
    }

    protected void MovePack(float x, float y) {
        Vector2 pos = new Vector2(x, y); 
        transform.position = Vector3.MoveTowards(transform.position, pos, packSpeed * Time.deltaTime);
    }

    public float GetFaith() { 
        return packFaith;
    }

    private void SetFaith() {
        float x = 0;
        foreach (DogBase dog in dogs) { 
            x += dog.GetFaith();
        }
        packFaith = x;
    }
}
