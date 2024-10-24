using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DogPack : DogPack
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update(){

        if (Input.GetKeyDown(KeyCode.Mouse0)){
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            MoveFlag(mousePos);
        }

       SetPos();
    }

}
