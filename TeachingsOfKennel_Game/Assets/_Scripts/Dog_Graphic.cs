using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Graphic : MonoBehaviour
{
    [SerializeField] private GameObject flag;

    [Range(1, 100)]
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, flag.transform.position, speed * Time.deltaTime);
    }
}
