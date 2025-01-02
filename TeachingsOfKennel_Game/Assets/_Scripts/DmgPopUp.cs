using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class DmgPopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    [SerializeField] private float ySpeed, rotSpeed, despawnTime;

    private void Awake(){
        textMesh = GetComponent<TextMeshPro>();
    }

    public void SetUp(float amount){
         textMesh.text = amount.ToString();
    }

    private void Update(){
        transform.position += new Vector3(0, ySpeed, 0) * Time.deltaTime;
        despawnTime -= 1 * Time.deltaTime;
        if (despawnTime <= 0) {  
            Destroy(gameObject);
        }
    }
}
