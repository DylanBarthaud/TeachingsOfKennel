using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private ButtonList buttonList;  

    public void SpawnObject(GameObject caller, int numberToSpawn, int numberOfItems)
    {
        throw new System.NotImplementedException();
    }

    public void SpawnObject(GameObject caller, int numberToSpawn, int[] listOfItemIds)
    {
        throw new System.NotImplementedException();

        for (int i = 0; i < numberToSpawn; i++) {  
            Instantiate(buttonList.GetButtons(listOfItemIds[i]));
        }
    }

    public void SpawnObject(GameObject caller, int numberToSpawn, int randX, int randY)
    {
        throw new System.NotImplementedException();
    }
}
