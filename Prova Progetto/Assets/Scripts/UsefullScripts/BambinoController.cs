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
        // 15 secondi dopo di che ciao ciao soldini
        yield return new WaitForSecondsRealtime(15);
        for(int i = 0; i<_coins.Length;i++){
            if(_coins[i] != null)
                Destroy(_coins[i]);
        }
    }

    IEnumerator coroutine(){
        // Si attiva quando "finisco" le cose nella scena che posso "marachellare"
        // Dopo 10 secondi carica lo storytelling che introduce la scena dell'inseguimento 
        yield return new WaitForSecondsRealtime(10);
        SceneManager.LoadScene(GameEvent.STORYTELLING_INSEGUIMENTO);        
    }



    // Nota: Ho messo dei *3 su tutti gli Spawn delle monete per 
    void Update(){
        /*if (E){
            
            // Attiviamo il "listener" sulla E solo quando abbiamo ricevuto un "onTriggerEnter"
            // tramite il sistama di Broadcasting
            if (Input.GetKeyUp(KeyCode.E)){
                if (tagInteraction == GameEvent.FANTASMINO_TAG){
                    Messenger.Broadcast(GameEvent.START_TUTORIAL);
                }
                else if (tagInteraction == GameEvent.PASTELLI_TAG){
                    // La prima marachella e' sempre pastelli perche' indotta dal tutorial
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
                    // Sui tag faccio sempre "-1" perche' i GameEvent sono numerati a partire da 1
                    if (inventary[GameEvent.TELECOMANDO_INDEX-1] >0 ){
                        spawnCoin(inventary[GameEvent.TELECOMANDO_INDEX-1]*3);            
                        audio.releaseObject(flushAudio);
                        LasciaOggetto(GameEvent.TELECOMANDO_INDEX);
                        // Facciamo arrivare il messaggio alla UI
                        MissionComplete();
                        // Facciamo in modo che scompaiano i soldi
                        StartCoroutine(corutine());
                    }
                } 
                else if (tagInteraction == GameEvent.FRIGO_TAG){    
                    spawnCoin(inventary[GameEvent.PASTELLI_INDEX-1]*3);
                    if(primaMarachella)
                    {
                        if (inventary[GameEvent.PASTELLI_INDEX-1] >0 ){
                        MissionComplete();
                        primaMarachella=false;
                        }
                    }else{
                        if (inventary[GameEvent.PASTELLI_INDEX-1] >0 ){
                            MissionComplete();
                        }
                    }                 
                    audio.releaseObject(fridgeAudio);
                    LasciaOggetto(GameEvent.PASTELLI_INDEX);
                    StartCoroutine(corutine());
                }
                else if (tagInteraction == GameEvent.BRUCIA_TAG){
                    if (inventary[GameEvent.BOOKS_INDEX-1] >0 ){
                        spawnCoin(inventary[GameEvent.BOOKS_INDEX-1]*3);
                        LasciaOggetto(GameEvent.BOOKS_INDEX);
                        MissionComplete();
                        StartCoroutine(corutine());
                    }
                }  
            }
        }
            */
    }

    public void UpdateDiavoletto(int i) {
        audio.collect(collectCoinAudio);
        diavoletto_score+=i;
        Messenger.Broadcast(GameEvent.DIAVOLETTO_UPDATE);
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

    // Per comunicare l'evento alla UI e aggiornare i testi del tutorial
    public void PrimaMarachella(){
        primaMarachella = true;
        // Manda un messaggio broadcast per "avviare" il metodo a cui è associata la costante "PRIMA_MARACHELLA"
        Messenger.Broadcast(GameEvent.PRIMA_MARACHELLA);
    }

    public void DeactivateE(Collider objectRecived){
        this.E = false;
        this.tagInteraction = UIMessages.EMPTY_MESSAGE;
        Transform transform = objectRecived.transform;
        foreach(Transform t in transform){
            // Dobbiamo "cercare" l'oggetto Canvas con la "E" sopra
            if(t.gameObject.name.Contains("Canvas"))
                t.gameObject.SetActive(false);
        }
    }


    // La size "determina" il numero di numeri casuali (da 1 a 10) la cui somma sara il numero di monete spawnate
    // Giusto per rendere piu proporzionale il numero di monete spawnate
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
