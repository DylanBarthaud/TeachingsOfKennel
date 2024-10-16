using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Graphic : MonoBehaviour
{
    Vector3 movePos;
    float dogGraphicSpeed; 

    public void SetSpeed(float speed)
    {
        dogGraphicSpeed = speed;
    }

    public void MoveDogGraphic(Vector3 movePos, float speed)
    {
        this.movePos = movePos;
        dogGraphicSpeed = speed; 
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos, dogGraphicSpeed * Time.deltaTime);
    }
}
