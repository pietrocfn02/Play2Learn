using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//to do something
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoControllerAngiolettoMode : MonoBehaviour {
    
    private int initialSize = 0;
    private int diavoletto_score = 0;
    private int angioletto_score = 0;
    private int[] inventary = {0, 0, 0}; // Telecomando, Pastelli, Foglio
    private bool E = false;
    private CharacterController _charController;
    private string tagInteraction = "";
    [SerializeField] private GameObject goodCoinPrefab;
    [SerializeField] private Material newMaterial;
    private GameObject[] _coins;
    private GameObject[] tv;
    private Collider objectToDestroy;
    private bool nextMission = false;
    private int count = 0;
    
    void Start(){
        
    }

    IEnumerator corutine(){
        yield return new WaitForSecondsRealtime(6);
        for(int i = 0; i<_coins.Length;i++){
            if(_coins[i] != null)
                Destroy(_coins[i]);
        }
        _coins = new GameObject [initialSize];
    }


    void Update(){
        if (E){
            if (Input.GetKeyUp(KeyCode.E)){
                E = false;
                if (tagInteraction == "Pastelli"){
                    RaccoltoOggetto(2);
                }else if (tagInteraction == "Contenitore"){
                    if (inventary[1] >= 12){
                        spawnCoin(inventary[1]);                        
                        LasciaOggetto(2);
                        MissionComplete(GameEvent.MISSIONE_PASTELLI);
                    }
                    StartCoroutine(corutine());       
                }else if (tagInteraction == "Telecomando"){
                    RaccoltoOggetto(1);
                }else if (tagInteraction.Contains("TV_")){
                    if (inventary[0] >= 1){
                        TurnOffTV(tagInteraction);
                        if (count >= 4){
                            count = 0;
                            spawnCoin(count);
                            MissionComplete(GameEvent.MISSIONE_TELEVISIONI);
                        }
                    }
                }
            }
        }
    }

    public void TurnOffTV(string tag){
        tv = GameObject.FindGameObjectsWithTag(tag);
        tv[0].GetComponent<Renderer>().material = newMaterial;
        count ++;
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

    public void spawnCoin(int size){
        int[] randomArray = new int[size];
        int randomSum = 0;
        for (int i=0 ; i < randomArray.Length; i++) {
            randomArray[i] = Random.Range(0,11);
            randomSum+= randomArray[i];
        }
        _coins = new GameObject[randomSum];
         for(int i=0; i<_coins.Length; i++)
        {
            if(_coins[i] == null)
            {
                GameObject x = Instantiate(goodCoinPrefab) as GameObject;
                Vector3 bimboPosition = this.transform.position;
                Vector3 spawnPosition = new Vector3(Random.Range(bimboPosition.x-2,bimboPosition.x+2), 1.75f, Random.Range(bimboPosition.z-2,bimboPosition.z+2));
                x.transform.position = spawnPosition;
                _coins[i] = x;
                _coins[i].transform.localScale += new Vector3(3f,3f,1f);
                float angle = Random.Range (0, 360f);
                _coins[i].transform.Rotate(0, angle, 0);
            }
        }  
        inventary[1] = 0;
    }

    public void MissionComplete(string missionTag){
        Messenger.Broadcast(missionTag);
    }
}
