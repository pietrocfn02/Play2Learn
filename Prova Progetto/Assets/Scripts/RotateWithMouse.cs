using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RotateWithMouse : MonoBehaviour
{
    private Vector3 mousePos;
    private float rotationSpeed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        this.transform.Rotate(rotX, rotY,0f);
        //this.transform.Rotate(Vector3.right, rotX);
    }
}
