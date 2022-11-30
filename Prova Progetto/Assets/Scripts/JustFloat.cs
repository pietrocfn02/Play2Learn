using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class JustFloat : MonoBehaviour
{
 
    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 0.5f;

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    
    void Start(){
        posOffset = transform.position;
    }
    void Update()
    {
        Float(); 
    }
    
    private void Float(){
       
        tempPos = posOffset;
        tempPos.x = transform.position.x;
        tempPos.z = transform.position.z;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
    
    void OnDestroy() {
    }
}
