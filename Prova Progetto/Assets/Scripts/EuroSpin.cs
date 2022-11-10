using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuroSpin : MonoBehaviour
{
    public Space relativeTo = Space.Self;
    public float speed = 1f;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(0, speed, 0, Space.World);
        //transform.Translate (0, speed, 0);
    }
}
