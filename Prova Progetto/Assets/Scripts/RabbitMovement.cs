using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private Transform camera;
    void Start(){}

    void Update(){
        transform.LookAt(camera);
        
    }
}
