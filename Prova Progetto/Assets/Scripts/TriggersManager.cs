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
                                        GameEvent.SIGN1_TAG, 
                                        GameEvent.SIGN2_TAG,
                                        GameEvent.SIGN3_TAG,
                                        GameEvent.SIGN4_TAG,
                                        GameEvent.SIGN5_TAG,
                                        GameEvent.SIGN6_TAG,
                                        GameEvent.SIGN7_TAG,
                                        GameEvent.TRIANGLE_TAG,
                                        GameEvent.CLIPBOARD_TAG,
                                        GameEvent.CONE_TAG,
                                        GameEvent.ARROW0_TAG,
                                        GameEvent.ARROW1_TAG,
                                        GameEvent.ARROW2_TAG,
                                        GameEvent.ARROW3_TAG,
                                        GameEvent.TAPE_TAG,
                                        GameEvent.CALC_TAG,
                                        GameEvent.COLLECTABLE_TAG
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
