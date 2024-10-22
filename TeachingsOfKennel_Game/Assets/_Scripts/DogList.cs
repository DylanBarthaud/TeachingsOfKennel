using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogList : MonoBehaviour 
{

    [SerializeField]
    private List<GameObject> list = new List<GameObject>();

    public GameObject GetDog(int id)
    {
        foreach(GameObject dogObject in list)
        {
            DogBase dog = dogObject.GetComponent<DogBase>();
            if(dog.GetId() == id)
            {
                return dogObject;
            }
        }

        return null;
    }

    public GameObject GetRandomDog()
    {
        int r = Random.Range(0, list.Count);
        return list[r];
    }
}
