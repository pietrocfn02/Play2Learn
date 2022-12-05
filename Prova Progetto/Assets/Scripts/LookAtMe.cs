using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scriptino da attaccare agli oggetti di tipo "canvas" che voglio che seguano il personaggio
public class LookAtMe : MonoBehaviour
{
    // Guarda ciò che gli è stato passato nell'ispector
    public Transform look;
    void Update(){
        transform.LookAt(look);
    }
}
