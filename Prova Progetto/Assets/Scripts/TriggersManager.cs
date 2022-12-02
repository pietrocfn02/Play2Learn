using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersManager : MonoBehaviour
{
    private List<string> tagsList = new List<string>{
                                        GameEvent.FANTASMINO_TAG,
                                        GameEvent.TELECOMANDO_TAG,
                                        GameEvent.PASTELLI_TAG,
                                        GameEvent.BOOKS_TAG,
                                        GameEvent.WATER_TAG,
                                        GameEvent.BRUCIA_TAG,
                                        GameEvent.FRIGO_TAG,
                                        GameEvent.CONTENITORE_TAG,
                                        GameEvent.TV_BAGNO_TAG,
                                        GameEvent.TV_CAMERA_LETTO_TAG,
                                        GameEvent.TV_SALA_GIOCHI_TAG,
                                        GameEvent.TV_CUCINA_TAG
                                        };
    

    void OnTriggerEnter(Collider other) {
        if(tagsList.Contains(other.tag))
        {
            BroadcastMessage("ActivateE",other);
        }
        else if (other.tag == GameEvent.EVIL_COIN_TAG)
        {
            BroadcastMessage("UpdateDiavoletto", 10);
            Destroy(other.gameObject);
        }
        else if (other.tag == GameEvent.GOOD_COIN_TAG)
        {
            BroadcastMessage("UpdateAngioletto", 10);
            Destroy(other.gameObject);
        }
    }
    


    void OnTriggerExit(Collider other) {
        if(tagsList.Contains(other.tag))
            BroadcastMessage("DeactivateE",other);
    }
}
