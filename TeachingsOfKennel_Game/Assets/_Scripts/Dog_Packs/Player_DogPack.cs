using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DogPack : DogPack
{
    [SerializeField] private Camera mainCamera;
    private Vector3 newFlagPos;

    private void Awake()
    {
        GameObject dog = GlobalDogList.instance.GetDog(1);
        AddDog(dog.GetComponent<DogBase>());

        GameObject dog2 = GlobalDogList.instance.GetDog(1);
        AddDog(dog2.GetComponent<DogBase>());
    }

    private void Update(){

        //Place flag for pack to move towards
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            //Add graphic for mouse click
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newFlagPos = mousePos;
        }

        if (newFlagPos != new Vector3(0, 0, 0))
        {
            MoveFlag(newFlagPos);
            MoveGraphics();
        }
    }
}
