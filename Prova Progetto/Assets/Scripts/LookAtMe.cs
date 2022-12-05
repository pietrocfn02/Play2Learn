using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scriptino da attaccare agli oggetti di tipo "canvas" che voglio che seguano il personaggio
public class LookAtMe : MonoBehaviour
{
    public Transform look;
    void Update(){
        transform.LookAt(look);
    }
}
