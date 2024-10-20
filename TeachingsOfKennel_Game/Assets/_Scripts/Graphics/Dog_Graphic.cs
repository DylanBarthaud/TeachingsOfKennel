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
        Vector3 newMovePos = new Vector3(movePos.x, movePos.y, 1);
        transform.position = Vector3.MoveTowards(transform.position, newMovePos, dogGraphicSpeed * Time.deltaTime);
    }

    public Vector3 getPos(){
        return transform.position;
    }
}
