using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{




    public void activateMe() {
        
        this.gameObject.SetActive(true);
        

    }

    public void deactivateMe() {
        this.gameObject.SetActive(false);
    }


    void Start() {
          this.gameObject.SetActive(false);
    }

    void Update() {

    }   

}