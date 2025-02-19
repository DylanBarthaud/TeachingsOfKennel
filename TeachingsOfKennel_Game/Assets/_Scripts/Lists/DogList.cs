using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogList : MonoBehaviour 
{

    [SerializeField]
    private List<GameObject> list = new List<GameObject>();

    public GameObject GetDog(int id){
        foreach(GameObject dogObject in list){
            DogBase dog = dogObject.GetComponent<DogBase>();
            if(dog.GetBreedId() == id){
                return dogObject;
            }
        }
        return null;
    }

    public GameObject GetRandomDog(int minId, int maxId){
        int r = Random.Range(minId, maxId);
        return list[r];
    }

    public int GetListLength(){
        return list.Count; 
    }
}
