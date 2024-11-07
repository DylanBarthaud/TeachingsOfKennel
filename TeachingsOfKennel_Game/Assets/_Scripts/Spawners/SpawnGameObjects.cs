using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjects : MonoBehaviour
{
    // Spawns multiple game objects at a given location or transform 
    // Returns game objects that it spawns 

    public GameObject[] SpawnGameObject(GameObject gameObject, int amount, Transform transform){
        GameObject[] gameObjects = new GameObject[amount]; 

        for (int i = 0; i < amount; i++){  
            gameObjects[i] = Instantiate(gameObject, transform);
        }
        return gameObjects;
    }
}
