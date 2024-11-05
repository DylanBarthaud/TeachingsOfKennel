using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogStatButtons : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private ButtonSpawner spawner;

    public void SpawnStatButtons(List<ISpawnsButtons> dogs){
        spawner.RemoveButtons(container.transform);
        spawner.SpawnButton(dogs, dogs.Count, 1, container.transform);
    }
}
