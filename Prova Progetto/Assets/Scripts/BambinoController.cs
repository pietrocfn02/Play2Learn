using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoController : MonoBehaviour {

    [SerializeField] private TMP_Text labelText;
    [SerializeField] private GameObject textWindow;
    [SerializeField] private GameObject evilCoinPrefab;

    private int diavoletto_score = 0;
    private int angioletto_score = 0;
    private int[] inventary = {0, 0, 0};
    private bool E = false;
    private CharacterController _charController;
    private string tagInteraction = "";
    private Collider objectToDestroy;
    private GameObject[] _coins;
    private bool primaMarachella = false;
    private int numOggettiLasciati=0;
    private int numTotOggetti=8;

    private AudioManager audio = new AudioManager();
    [SerializeField] public AudioClip collectAudio;
    [SerializeField] public AudioClip collectCoinAudio;
    [SerializeField] public AudioClip flushAudio;
    [SerializeField] public AudioClip fridgeAudio;
    
    void Start(){

    }

    IEnumerator corutine(){
        yield return new WaitForSecondsRealtime(15);
        for(int i = 0; i<_coins.Length;i++){
            if(_coins[i] != null)
                Destroy(_coins[i]);
        }
    }

    IEnumerator coroutine(){
        yield return new WaitForSecondsRealtime(10);
        SceneManager.LoadScene(GameEvent.STORYTELLING_INSEGUIMENTO);        
    }

    void Update(){
        if (E){
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
                    spawnCoin(inventary[GameEvent.TELECOMANDO_INDEX-1]);            
                    audio.releaseObject(flushAudio);
                    LasciaOggetto(GameEvent.TELECOMANDO_INDEX);
                    MissionComplete();
                    StartCoroutine(corutine());
                } 
                else if (tagInteraction == GameEvent.FRIGO_TAG){    
                    spawnCoin(inventary[GameEvent.PASTELLI_INDEX-1]);
                    if(primaMarachella)
                    {
                        FirstMissionComplete();
                        primaMarachella=false;
                    }else{
                        MissionComplete();
                    }                 
                    audio.releaseObject(fridgeAudio);
                    LasciaOggetto(GameEvent.PASTELLI_INDEX);
                    StartCoroutine(corutine());
                }
                else if (tagInteraction == GameEvent.BRUCIA_TAG){
                    spawnCoin(inventary[GameEvent.BOOKS_INDEX-1]);
                    LasciaOggetto(GameEvent.BOOKS_INDEX);
                    MissionComplete();
                    StartCoroutine(corutine());
                }  
            }
        }
    }

    public void UpdateDiavoletto(int i) {
        audio.collect(collectCoinAudio);
        diavoletto_score+=i;
        Messenger.Broadcast(GameEvent.DIAVOLETTO_UPDATE);
    }

    public void UpdateAngioletto(int i) {
        angioletto_score+=i;
        Messenger.Broadcast(GameEvent.ANGIOLETTO_UPDATE);
    }

    public void RaccoltoOggetto(int i){
        audio.collect(collectAudio);
        inventary[i-1] += 1;
        if(objectToDestroy!= null){
            Transform childText = objectToDestroy.gameObject.GetComponent<Transform>();
            Destroy(objectToDestroy.gameObject);
            objectToDestroy = null;
            tagInteraction = "";
        }
        Messenger.Broadcast(GameEvent.RACCOLTA_UPDATE);       
    }

    public void LasciaOggetto(int i){        
        if(inventary[i-1]>0){
            numOggettiLasciati+=inventary[i-1];
            inventary[i-1]=0;
        }            
        Messenger.Broadcast(GameEvent.LANCIA_OGGETTO);

        if(numOggettiLasciati==numTotOggetti){
            StartCoroutine(coroutine());
        }
    }

    public void MissionComplete(){
        Messenger.Broadcast(GameEvent.MISSION_COMPLETE);
    }

    public void FirstMissionComplete(){
        Messenger.Broadcast(GameEvent.FIRST_MISSION_COMPLETE);
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
        primaMarachella = true;
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
                GameObject x = Instantiate(evilCoinPrefab) as GameObject;
                Vector3 bimboPosition = this.transform.position;
                Vector3 spawnPosition = new Vector3(Random.Range(bimboPosition.x-1,bimboPosition.x+1), 1.7f, Random.Range(bimboPosition.z-1,bimboPosition.z+1));
                x.transform.position = spawnPosition;
                _coins[i] = x;
                _coins[i].transform.localScale += new Vector3(3f,3f,1f);
                float angle = Random.Range (0, 360f);
                _coins[i].transform.Rotate(0, angle, 0);
            }
        }  
    }
}
