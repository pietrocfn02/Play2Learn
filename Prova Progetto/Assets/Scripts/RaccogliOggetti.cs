using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccogliOggetti : MonoBehaviour
{
              
        void OnTriggerEnter(Collider other) {
            BroadcastMessage("RaccoltoTelecomando", 1);
            Debug.Log(other.gameObject.ToString());
            Destroy(other.gameObject);
        }


        void OnTriggerExit(Collider other) {
        }
}
