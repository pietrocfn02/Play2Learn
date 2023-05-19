using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RotateWithMouse : MonoBehaviour
{
    
    private float rotationSpeed = 150f;
    void Start()
    {
        
    }

    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        this.transform.Rotate(rotY, rotX,0f);
        //this.transform.Rotate(Vector3.right, rotX);
    }
}
