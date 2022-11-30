using UnityEngine;
using System.Collections;
using TMPro;
//to do something
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoControllerAngiolettoMode : MonoBehaviour {

    private int diavoletto_score = 0;
    private int angioletto_score = 0;
    private int[] inventary = {0, 0, 0};
    private bool E = false;
    private CharacterController _charController;
    private string tagInteraction = "";

    private Collider objectToDestroy;
    void Start(){

    }

    void Update(){
        if (E){
            if (Input.GetKeyUp(KeyCode.E)){
                Debug.Log("PREMO E");
                E = false;

                if (tagInteraction == "Pastelli"){
                    RaccoltoOggetto(2);
                }
                if (tagInteraction == "Contenitore"){
                    if (inventary[1] >= 12){
                        Debug.Log(inventary[1]);
                        LasciaOggetto(2);
                        MissionComplete();
                    }
                }
                
            }
        }
    }

    public void UpdateDiavoletto(int i) {
        diavoletto_score+=i;
        Messenger.Broadcast(GameEvent.DIAVOLETTO_UPDATE);
    }

    public void UpdateAngioletto(int i) {
        angioletto_score+=i;
        Messenger.Broadcast(GameEvent.ANGIOLETTO_UPDATE);
    }

    public void RaccoltoOggetto(int i){
        inventary[i-1] += 1;
        if(objectToDestroy!= null){
            Transform childText = objectToDestroy.gameObject.GetComponent<Transform>();
            Debug.Log(childText.GetType());
            Destroy(objectToDestroy.gameObject);
        }
        Messenger.Broadcast(GameEvent.RACCOLTA_UPDATE);

    }

    public void LasciaOggetto(int i){
        if(inventary[i-1]>0){
            inventary[i-1]=0;
        }
        Messenger.Broadcast(GameEvent.LANCIA_OGGETTO);
    }

    public int getOggettoCount(int i){
        return inventary[i];
    }

    public int getAngiolettoScore() {
        return angioletto_score;
    }

    public int getDiavolettoScore() {
        return diavoletto_score;
    }

    public void ActivateE(Collider objectRecived){
        this.E = true;
        this.tagInteraction = objectRecived.tag;
        this.objectToDestroy = objectRecived;
        Transform transform = objectRecived.transform;
        foreach(Transform t in transform){
            t.gameObject.SetActive(true);
        }
    }
    public void DeactivateE(Collider objectRecived){

        this.E = false;
        this.tagInteraction = "";
        Transform transform = objectRecived.transform;
        foreach(Transform t in transform){
            t.gameObject.SetActive(false);
        }
    }

    public void MissionComplete(){
        Messenger.Broadcast(GameEvent.MISSIONE_PASTELLI);
    }
}
