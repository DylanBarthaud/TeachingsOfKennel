using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DogPack : DogPack
{
    private void Update(){

        if (Input.GetKeyDown(KeyCode.Mouse0)){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveFlag(mousePos);
        }

       SetPos();
    }

}
