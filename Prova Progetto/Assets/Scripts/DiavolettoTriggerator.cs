using UnityEngine;
using System.Collections;
public class DiavolettoTriggerator : MonoBehaviour {
                
        void OnTriggerEnter(Collider other) {
            BroadcastMessage("UpdateDiavoletto", 1);
            Destroy(other.gameObject);
            //foreach (GameObject target in targets) {
                
                //target.SendMessage("Collected coin");
            //}
        }


        void OnTriggerExit(Collider other) {
        }


       /* public void Activate() {
            if (!_open) {
                Vector3 pos = transform.position + dPos;
                transform.position = pos;
                _open = true;
            }
        }

        public void Deactivate() {
            if (_open) {
                Vector3 pos = transform.position - dPos;
                transform.position = pos;
                _open = false;
            }
        }*/
}