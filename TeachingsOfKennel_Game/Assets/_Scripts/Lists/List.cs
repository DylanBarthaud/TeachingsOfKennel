using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List : MonoBehaviour
{
    [SerializeField] private GameObject[] list; 

    public GameObject ReturnItem(int index){
        return list[index];
    }

    public GameObject ReturnRandomItem(int minIndex, int maxIndex) {  
        int r = Random.Range(minIndex, maxIndex);
        return ReturnItem(r);
    }

    public int ReturnListLength(){
        return list.Length;
    }
}
