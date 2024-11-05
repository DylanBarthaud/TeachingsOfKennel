using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonToTarget : MonoBehaviour
{
    private GameObject target;
    private Camera mainCamera;
    private RectTransform buttonRectTransform;

    private void Awake(){
        mainCamera = Camera.main;
        buttonRectTransform = GetComponent<RectTransform>();
    }

    public void SetTarget(GameObject target){
        this.target = target;
    }

    private void Update(){
        if (this.target != null){
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.transform.position);
            buttonRectTransform.position = screenPos;
        }

        else{  
            Destroy(gameObject);
        }
    }
}
