using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersManager : MonoBehaviour
{
    private List<string> tagsList = new List<string>{
                                        GameEvent.FANTASMINO_TAG,
                                        GameEvent.TELECOMANDO_TAG,
                                        GameEvent.PASTELLI_TAG,
                                        GameEvent.BOOKS_TAG
                                        };
    

    void OnTriggerEnter(Collider other) {
        if(tagsList.Contains(other.tag))
        {
            BroadcastMessage("ActivateE",other);
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
            Debug.Log("OGGETTO TAG DIVERSO");
        }
    }


    void OnTriggerExit(Collider other) {
        if(tagsList.Contains(other.tag))
            BroadcastMessage("DeactivateE",other);
    }
}
