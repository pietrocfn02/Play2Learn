using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CulturaOrbitante : MonoBehaviour {

 
    [SerializeField] private Transform center;
    private Vector3 axis = Vector3.up;
    private float radius = 0.3f;
    private float radiusSpeed = 0.5f;
    private float rotationSpeed = 80.0f; 

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();


    private float amplitude = 0.05f;
    private float frequency = 0.5f;
 
    void Start() {
        transform.position = (transform.position - center.position).normalized * radius + center.position;

        posOffset = transform.position;
    }
 
    void Update() {
        transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        Vector3 desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        Float();
    }

    private void Float(){
       
        tempPos = posOffset;
        tempPos.x = transform.position.x;
        tempPos.z = transform.position.z;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

}
