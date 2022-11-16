using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoController : MonoBehaviour {

    private int diavoletto_score = 0;
    private int angioletto_score = 0;
    private int[] inventary = {0, 0, 0};

    private CharacterController _charController;

    void Start()
    {

    }

    void Update()
    {

    }

    public void UpdateDiavoletto(int i) {
        diavoletto_score+=i;
        Messenger.Broadcast(GameEvent.DIAVOLETTO_UPDATE);

        Debug.Log(diavoletto_score);
    }

    public void UpdateAngioletto(int i) {
        angioletto_score+=i;
        Messenger.Broadcast(GameEvent.ANGIOLETTO_UPDATE);

        Debug.Log(angioletto_score);
    }

    public void RaccoltoOggetto(int i){
        inventary[i-1] += 1;
        Messenger.Broadcast(GameEvent.RACCOLTA_UPDATE);
    }

    public void LasciaOggetto(int i){
        if(inventary[i-1]>0)
            inventary[i-1]--;
        Messenger.Broadcast(GameEvent.LANCIA_OGGETTO);
    }
    
    public void AvviaTutorial(){
        Messenger.Broadcast(GameEvent.START_TUTORIAL);
    }

    public void ActivateCamera(string camera){
        
        Debug.Log("########### "+camera+" ############");
        Messenger.Broadcast(GameEvent.ACTIVATE_CAMERA+camera);
    }

    public void DeactivateCamera(string camera){
        Debug.Log("########### "+camera+" ############");
        Messenger.Broadcast(GameEvent.DEACTIVATE_CAMERA+camera);
        
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


}
