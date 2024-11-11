using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Engine : MonoBehaviour
{
    private Utilities utilities = new Utilities(); 

    public static Game_Engine instance;

    [SerializeField] UiManager uiManager;
    [SerializeField] PacksManager packsManager;
    
    private void Awake(){
        if(instance == null){
            instance = this;
        }

        GlobalEventSystem eventSystem = GlobalEventSystem.instance;

        packsManager.SpawnPack(packsManager.playerPackTemplate, 1, new int[] { 5 }, new Vector3(0, 0, 0)); 
    }
}
