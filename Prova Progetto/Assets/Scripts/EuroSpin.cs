using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuroSpin : MonoBehaviour
{

    // Script di rotazione delle monete

    // EURO = SOLDI
    // SPIN = ROTAZIONE :)

    
    public Space relativeTo = Space.Self;
    public float speed = 1f;
   
    void Start()
    {
        
    }

    // Ruota le monte 
    void Update()
    {
        transform.Rotate(0, speed, 0, Space.World);
    }
}
