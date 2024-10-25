using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    public void SpawnObject(GameObject caller, int numberToSpawn, int numberOfItems);

    public void SpawnObject(GameObject caller, int numberToSpawn, int[] listOfItemIds);

    public void SpawnObject(GameObject caller, int numberToSpawn, int randX, int randY);
}
