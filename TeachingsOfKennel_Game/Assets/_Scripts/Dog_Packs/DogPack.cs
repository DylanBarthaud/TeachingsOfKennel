using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPack : MonoBehaviour
{
    protected List<DogBase> dogs = new List<DogBase>();
    protected float packFaith;
    protected float packSpeed = 2;

    [SerializeField] protected List<Dog_Graphic> dog_Graphics = new List<Dog_Graphic>();
    [SerializeField] private Dog_Graphic_Handler graphic_Handler; 

    protected Vector3 flagPos;

    public void AddDog(DogBase dog, Dog_Graphic dog_Graphic){
        dogs.Add(dog);
        dog_Graphics.Add(dog_Graphic); 
    }

    private void RemoveDog(DogBase dog){ 
        dogs.Remove(dog);
        dog_Graphics.Remove(dog.GetGraphic());
    }

    public void TickBarks(DogPack target){
        foreach (DogBase dog in dogs) {
            dog.bark(); 
        }
    }

    public void ConvertRandom(DogPack newPack){
        foreach (DogBase dog in dogs){
            int x = Random.Range(1, 101);
            if (x >= dog.GetFaith()) {
                //newPack.AddDog(dog);
                RemoveDog(dog);
            }
        }
    }

    protected void MoveFlag(Vector3 newFlagPos) {
        flagPos = newFlagPos;
        MoveGraphics(); 
    }

    public void MoveGraphics()
    {
        graphic_Handler.MoveAllDogGraphics(dog_Graphics, flagPos, packSpeed); 
    }

    protected void AssignGraphics()
    {
        dog_Graphics = new List<Dog_Graphic>();
        foreach (DogBase dog in dogs)
        {
            dog_Graphics.Add(dog.GetGraphic());
        }
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

    private void SetSpeed() {
        float x = 0;
        foreach (DogBase dog in dogs) { 
            x += dog.GetSpeed();
        }
        packSpeed = x / dogs.Count;
    }
}
