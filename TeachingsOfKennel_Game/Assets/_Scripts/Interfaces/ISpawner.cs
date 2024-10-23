using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    public void SpawnObject(GameObject caller, int numberOfItems);

    public void SpawnObject(GameObject caller, int[] listOfItemIds);
}
