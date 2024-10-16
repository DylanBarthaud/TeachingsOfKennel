using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DogPack : DogPack
{
    [SerializeField] private Camera mainCamera;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            MovePack(mousePos.x, mousePos.y);
        }
    }


}
