using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RotateWithMouse : MonoBehaviour
{
    
    private float rotationSpeed = 150f;
    
    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
       
        transform.RotateAround(Vector3.up, -rotX);
        // transform.RotateAround(Vector3.right, rotY);
    }
}
