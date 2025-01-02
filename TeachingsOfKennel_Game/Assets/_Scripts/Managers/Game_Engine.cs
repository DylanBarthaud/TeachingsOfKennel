using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Engine : MonoBehaviour
{
    private Utilities utilities = new Utilities(); 

    public static Game_Engine instance;

    [SerializeField] PacksManager packsManager;
    [SerializeField] UiManager uiManager;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }

        GlobalEventSystem eventSystem = GlobalEventSystem.instance;

        int[] amountOfDogs = new int[] { 15 };
        int[] dogIds = new int[] { 2 }; 
        packsManager.SpawnPack(packsManager.playerPackTemplate, 1, amountOfDogs, dogIds, new Vector3(0, 0, 0)); 
    }
}
