using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    void Start() {
         this.findAndSetActive(false);
    }

    void Update() {

    }   

    void Awake() {
        Messenger.AddListener("ACTIVATE_CAMERA_Camera_Corridoi", activateMe);
        Messenger.AddListener("DEACTIVATE_CAMERA_Camera_Corridoi", deactivateMe);
    }


    private void findAndSetActive(bool value) {
        GameObject[] toActivate =  GameObject.FindGameObjectsWithTag(this.tag);
        foreach (GameObject g in toActivate)
        {
            g.SetActive(value);
        }
    }


    private void activateMe() {
        
        Debug.Log("Mi sto attivando!");
        Debug.Log("Sono "+this.tag);
        this.findAndSetActive(true);
        

    }

    private void deactivateMe() {
        Debug.Log("Mi sto disattivando!");
        this.findAndSetActive(false);
    }

}