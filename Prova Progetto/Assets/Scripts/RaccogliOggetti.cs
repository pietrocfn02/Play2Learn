using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccogliOggetti : MonoBehaviour
{
        void OnTriggerEnter(Collider other) {
            if(other.tag == "Fantasmino"){
                BroadcastMessage("ActivateE",other.tag);
            }
            if(other.tag == "Telecomando")
            {   
                BroadcastMessage("RaccoltoOggetto", 1);
                Debug.Log(other.gameObject.ToString());
                Destroy(other.gameObject);
            }
            else if (other.tag == "Pastelli")
            {
                //BroadcastMessage("RaccoltoOggetto", 1);
                //i pastelli sono in posizione 2 nella mia lsta di labels (texts)
                BroadcastMessage("RaccoltoOggetto", 2);
                Destroy(other.gameObject);
            }
            else if (other.tag == "Books")
            {
                //BroadcastMessage("RaccoltoOggetto", 2);
                //i pastelli sono in posizione 3 nella mia lsta di labels (texts)
                BroadcastMessage("RaccoltoOggetto", 3);
                Destroy(other.gameObject);
            }
            else if (other.tag == "EvilCoin")
            {
                BroadcastMessage("UpdateDiavoletto", 1);
                Destroy(other.gameObject);
            }
            else if (other.tag == "GoodCoin")
            {
                BroadcastMessage("UpdateAngioletto", 1);
                Destroy(other.gameObject);
            }
            else if (other.tag == "Door")
            {
                Debug.Log("PORTA!");
                Destroy(other.gameObject);
            }
            else if (other.tag == "Water")
            {
                BroadcastMessage("LasciaOggetto", 1);
            }
            else if (other.tag == "Brucia")
            {
                BroadcastMessage("LasciaOggetto", 3);
            }
            else if (other.tag == "Frigo")
            {
                BroadcastMessage("LasciaOggetto", 2);
            }
            else if (other.tag !=null && other.tag.Contains("Camera_")) 
            { 
                Debug.Log("Entering in action range of camera : "+other.tag);
                BroadcastMessage("ActivateCamera", other.tag);
            }
            else
            {
                Debug.Log("OGGETTO: "+other.gameObject.ToString());
            }
        }


        void OnTriggerExit(Collider other) {
            if(other.tag == "Fantasmino"){
                BroadcastMessage("DeactivateE",other.tag);
            }
        }
}
