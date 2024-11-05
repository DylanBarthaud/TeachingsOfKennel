using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonList : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons; 

    public GameObject GetButtons(int index){
        return buttons[index];
    }
}