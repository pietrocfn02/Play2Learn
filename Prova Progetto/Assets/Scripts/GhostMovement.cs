using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    
    [SerializeField] private Transform camera;
    [SerializeField] private Transform ghost;
    void Start(){}

    void Update(){
        transform.LookAt(camera);
        
        //transform.Translate(Vector3.up * Time.deltaTime);
    }
}
