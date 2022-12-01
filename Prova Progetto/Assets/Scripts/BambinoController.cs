using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoController : MonoBehaviour {

    [SerializeField] private TMP_Text labelText;
    [SerializeField] private GameObject textWindow;

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
            //this.E=false;
            if (Input.GetKeyUp(KeyCode.E)){
                if (tagInteraction == GameEvent.FANTASMINO_TAG){
                    Messenger.Broadcast(GameEvent.START_TUTORIAL);
                }
                else if (tagInteraction == GameEvent.PASTELLI_TAG){
                    PrimaMarachella();
                    RaccoltoOggetto(GameEvent.PASTELLI_INDEX);
                }
                else if (tagInteraction == GameEvent.TELECOMANDO_TAG){
                    RaccoltoOggetto(GameEvent.TELECOMANDO_INDEX);
                }
                else if (tagInteraction == GameEvent.BOOKS_TAG){
                    RaccoltoOggetto(GameEvent.BOOKS_INDEX);
                }
                else if (tagInteraction == GameEvent.WATER_TAG){
                    LasciaOggetto(GameEvent.TELECOMANDO_INDEX);
                } 
                else if (tagInteraction == GameEvent.FRIGO_TAG){
                    labelText.text = UIMessages.FINE_PRIMA_MARACHELLA;
                    labelText.gameObject.SetActive(true);
                    LasciaOggetto(GameEvent.PASTELLI_INDEX);
                }
                else if (tagInteraction == GameEvent.BRUCIA_TAG){
                    LasciaOggetto(GameEvent.BOOKS_INDEX);
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
            Destroy(objectToDestroy.gameObject);
            objectToDestroy=null;
        }
        Messenger.Broadcast(GameEvent.RACCOLTA_UPDATE);       
    }

    public void LasciaOggetto(int i){
        if(inventary[i-1]>0)
            inventary[i-1]=0;
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
    public void PrimaMarachella(){
        Messenger.Broadcast(GameEvent.PRIMA_MARACHELLA);
    }

    public void DeactivateE(Collider objectRecived){
        this.E = false;
        this.tagInteraction = UIMessages.EMPTY_MESSAGE;
        Transform transform = objectRecived.transform;
        foreach(Transform t in transform){
            if(t.gameObject.name.Contains("Canvas"))
                t.gameObject.SetActive(false);
        }
    }
}
