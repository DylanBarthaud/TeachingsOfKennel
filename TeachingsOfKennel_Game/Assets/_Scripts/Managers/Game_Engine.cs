using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveState { stored, active };
public enum State { freeRoam, fight, cooldown }
public enum StatType { health, barkStrength, barkSpeed, dogSpeed }
public class Game_Engine : MonoBehaviour
{
    private Utilities utilities = new Utilities(); 

    public static Game_Engine instance;

    [SerializeField] PacksManager packsManager;
    [SerializeField] UiManager uiManager;

    private Player_DogPack playerPack;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        GlobalEventSystem eventSystem = GlobalEventSystem.instance;

        int[] amountOfDogs = new int[] { 1 };
        int[] dogIds = new int[] { 1 }; 
        packsManager.SpawnPack(packsManager.playerPackTemplate, 1, amountOfDogs, dogIds, new Vector3(0, 0, 0));
        playerPack = GameObject.FindGameObjectWithTag("Player_Pack").GetComponent<Player_DogPack>();
    }

    public Player_DogPack GetPlayerPack(){
        return playerPack;
    }
}
