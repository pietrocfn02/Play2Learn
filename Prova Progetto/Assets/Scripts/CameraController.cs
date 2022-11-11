using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


     private List<CameraScript> cameras = new List<CameraScript>();
     private string[] tags = {"Camera_Corridoi","Camera_Ingresso"};

    void Start() {
       

    }

    void Update() {

    }   

    void Awake() {

        //Suppongo la corrispondenza tra i tag e le camere
         foreach (string tag in tags) {
            GameObject[] camerasTmp =  GameObject.FindGameObjectsWithTag(tag+"_CAM");
            if (camerasTmp.Length > 0) {
                cameras.Add(camerasTmp[0].GetComponent<CameraScript>());
            }
            
        }
        for (int i=0 ; i < tags.Length; i++) {
                Debug.Log("Entro qui!");
                Debug.Log(cameras.ToArray().Length);
                Messenger.AddListener("ACTIVATE_CAMERA_"+tags[i], cameras[i].activateMe);
                Messenger.AddListener("DEACTIVATE_CAMERA_"+tags[i], cameras[i].deactivateMe);
        } 
        
    }


    

}