using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersManager : MonoBehaviour
{

    //Gestione centralizzata dei tag per la collisione coi trigger
    private List<string> tagsList = new List<string>{
                                        GameEvent.FANTASMINO_TAG,   // Eliminare
                                        GameEvent.TELECOMANDO_TAG,
                                        GameEvent.PASTELLI_TAG,     // Eliminare
                                        GameEvent.BOOKS_TAG,        
                                        GameEvent.WATER_TAG,    
                                        GameEvent.BRUCIA_TAG,       // Eliminare
                                        GameEvent.FRIGO_TAG,        // Eliminare
                                        GameEvent.CONTENITORE_TAG,  // Eliminare
                                        GameEvent.TV_BAGNO_TAG,
                                        GameEvent.TV_CAMERA_LETTO_TAG,
                                        GameEvent.TV_SALA_GIOCHI_TAG,
                                        GameEvent.TV_CUCINA_TAG,
                                        GameEvent.TABLE_TAG,        // Eliminare
                                        GameEvent.TMP_TAG,
                                        "Door"
                                        };
    

    void OnTriggerEnter(Collider other) {
        // Gestisce l'attivazione della "E" per interagire con alcuni gli oggetti nella scena
        if(tagsList.Contains(other.tag))
        {
            Debug.Log("Entro in " + other.tag);
            BroadcastMessage("ActivateE",other);
        }
        else if (other.tag == GameEvent.EVIL_COIN_TAG)
        {
            // Gestisce l'update dei diavoletto coin mandando un BroadcastMessage con i relativi coin diavoletto
            // guadagnati e, in fine, distrugge l'oggetto
            BroadcastMessage("UpdateDiavoletto", 10);
            Destroy(other.gameObject);
        }
        else if (other.tag == GameEvent.GOOD_COIN_TAG)
        {
            // Gestisce l'update degli angioletto coin mandando un BroadcastMessage con i relativi coin angioletto
            // guadagnati e, in fine, distrugge l'oggetto
            BroadcastMessage("UpdateAngioletto", 10);
            Destroy(other.gameObject);
        }else if (other.tag == "Door"){
            // Manda un Messenger.Broadcast alla UIAngioletto
            Messenger.Broadcast(GameEvent.DOOR_EVENT);
        }
    }
    


    void OnTriggerExit(Collider other) {
        if (tagsList.Contains(other.tag))
        {
            Debug.Log("Esco da " + other.tag);
            BroadcastMessage("DeactivateE",other);
        }
    }
}
