using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersManager : MonoBehaviour
{

    //Gestione centralizzata dei tag per la collisione coi trigger
    private List<string> tagsList = new List<string>
    {
                                        GameEvent.TELECOMANDO_TAG,
                                        GameEvent.BOOKS_TAG,
                                        GameEvent.WATER_TAG,
                                        GameEvent.TV_BAGNO_TAG,
                                        GameEvent.TV_CAMERA_LETTO_TAG,
                                        GameEvent.TV_SALA_GIOCHI_TAG,
                                        GameEvent.TV_CUCINA_TAG,
                                        GameEvent.TABLE_TAG,
                                        GameEvent.MARK_TAG,
                                        GameEvent.EASEL_TAG,
                                        GameEvent.FLAG_TAG,
                                        GameEvent.VITRUVIAN_TAG,
                                        GameEvent.COLUMN_CORINTHIAN_TAG,
                                        GameEvent.COLUMN_IONIC_TAG,
                                        GameEvent.TOPOLINO_TAG,
                                        GameEvent.ONEPIECE_TAG,
                                        GameEvent.SNOOPY_TAG,
                                        GameEvent.SUPERMAN_TAG,
                                        GameEvent.TRIANGLE_TAG
    };
    

    void OnTriggerEnter(Collider other) {
        // Gestisce l'attivazione della "E" per interagire con alcuni gli oggetti nella scena
        if(tagsList.Contains(other.tag))
        {
            //Debug.Log("Entro in " + other.tag);
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
            //Debug.Log("Esco da " + other.tag);
            BroadcastMessage("DeactivateE",other);
        }
    }
}
