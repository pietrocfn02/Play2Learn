using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//to do something
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoControllerAngiolettoMode : MonoBehaviour {
    
    //Controller per azioni della "Angioletto_Scene"
    private int initialSize = 0;
    private int diavoletto_score = 0;
    private int angioletto_score = 0;
    //Array per l'inventario
    private int[] inventary = {0, 0, 0}; // Telecomando, Pastelli, Foglio
    private bool interact = false;
    private CharacterController _charController;
    private string tagInteraction = "";
    [SerializeField] private GameObject goodCoinPrefab;
    [SerializeField] private Material newMaterial;
    [SerializeField] private GameObject ghost;
    [SerializeField] public AudioClip clipTv;
    [SerializeField] public AudioClip pemAudio;
    [SerializeField] public AudioClip collectAudio;
    [SerializeField] public AudioClip collectCoinAudio;
    [SerializeField] public AudioClip releseAudio;
    private GameObject[] _coins;
    private GameObject[] tv;
    private Collider objectToDestroy;
    //private bool nextMission = false;
    private int count = 0;
    private AudioManager audio = new AudioManager();
    private bool tvComplete = false;

    void Start(){}

    IEnumerator corutine(){
        //Aspetta 15 secondi prima che i coin nella scena vengano eliminati
        yield return new WaitForSecondsRealtime(15);
        for(int i = 0; i<_coins.Length;i++){
            if(_coins[i] != null)
                Destroy(_coins[i]);
        }
        _coins = new GameObject [initialSize];
    }


    void Update(){
        // Attiviamo il "listener" sulla E solo quando abbiamo ricevuto un "onTriggerEnter"
        // tramite il sistama di Broadcasting

        /*if (interact){
            // Abbiamo premuto E
            // Verifichiamo con cosa possiamo interagire
            if (Input.GetKeyUp(KeyCode.E)){
                interact = false;
                if (tagInteraction == "Pastelli"){
                    // Il numerino si riferisce alla posizione dell'oggetto nell'inventario
                    RaccoltoOggetto(2);
                }else if (tagInteraction == "Contenitore"){
                    if (inventary[1] >= 12){
                        // I pastelli da raccogliere nella missione sono 12
                        // Richiama il metodo di raccolta
                        audio.releaseObject(releseAudio);
                        //Riproduce la clip inerente alla seconda missione
                        audio.reproducePem(pemAudio);
                        //Richiama il metodo che crea le monete nella scena
                        spawnCoin(inventary[1]);
                        //Richiama il metodo per "inserire" all'interno del contenitore i pastelli raccolti
                        LasciaOggetto(2);
                        MissionComplete(GameEvent.MISSIONE_PASTELLI);
                        //Elimina dalla scena il fantasma che blocca la porta della prima missione
                        Destroy(ghost);
                    }
                    //Avvia la coroutine sopra implementata
                    StartCoroutine(corutine());
                //Ripeto l'azione di raccolta per il telecomando
                }else if (tagInteraction == "Telecomando"){
                    RaccoltoOggetto(1);
                }else if (tagInteraction.Contains("TV_")){
                    //Controllo se il telecomando è stato preso dal player
                    if (inventary[0] >= 1){
                        //Spengo la TV "tagInteraction"
                        TurnOffTV(tagInteraction);
                        if (count >= 4){
                            // Le TV nella casa sono 4 --- faccio partire le azioni solo quando le ho spente tutte
                            spawnCoin(count*4);
                            count = 0;
                            audio.stopPem(pemAudio);
                            TurnOffTV(tagInteraction);
                            MissionComplete(GameEvent.MISSIONE_TELEVISIONI);
                        }
                    }
                    StartCoroutine(corutine());
                    //Controllo se nell'inventario è presente il telecomando per capire
                    //se avviare o meno la terza, e ultima, missione
                }else if (tagInteraction == "Books" && inventary[0] >= 1){
                    if(tvComplete) {
                        RaccoltoOggetto(3);    
                    }
                    else {
                        forgetSomething();
                    }
                }else if (tagInteraction == "Tavolo"){
                    forgetSomething();
                    //Controllo se il player ha collezionato il libro o meno
                    if (inventary[2] >= 1){
                        doHomework();
                    }else{
                        forgetSomething();
                    }
                }else if (tagInteraction == "Books" && inventary[0] < 1){
                    //Sembra ridondante ma ci vuole per non accavallare più missioni
                    forgetSomething();
                }
            }
        }*/
        Debug.Log("Premi [TAB] per iniziare!!");
        if(Input.GetKeyUp(KeyCode.Tab))
            Messenger.Broadcast(GameEvent.START_TUTORIAL);

    }

    // Per impedire che si facciano delle azioni senza i collezionabili necessari.
    // Appare un messaggio UI che ci avvisa
    public void forgetSomething(){
        Messenger.Broadcast(GameEvent.FORGET);
    }
    public void doHomework(){
        // Fermo il tempo per evitare che la bambina scappi mentre fa i compiti :)
        Time.timeScale = 0;
        CompitiConnector.siamoInModalitaCompiti = true;
        Messenger.Broadcast(GameEvent.MISSIONE_COMPITI);
    }

    // Metodo che cambia il materiale della tv su di cui è stato chiamato OnTriggerEnter
    public void TurnOffTV(string tag){
        tv = GameObject.FindGameObjectsWithTag(tag);
        tv[0].GetComponent<Renderer>().material = newMaterial;
        count ++;
        audio.turnOffTV(tag,clipTv);
    }
    // Aggiorna il punteggio e lo comunica alla UI
    public void UpdateAngioletto(int i) {
        audio.releaseObject(collectCoinAudio);
        angioletto_score+=i;
        Messenger.Broadcast(GameEvent.ANGIOLETTO_UPDATE);
    }
    // "Raccolgie" l'oggetto, lo comunica alla UI e in fine avvia la clip audio
    // per fare capire che l'oggetto è stato collezionato
    public void RaccoltoOggetto(int i){
        inventary[i-1] += 1;
        if(objectToDestroy!= null){
            Transform childText = objectToDestroy.gameObject.GetComponent<Transform>();
            Destroy(objectToDestroy.gameObject);
        }
        Messenger.Broadcast(GameEvent.RACCOLTA_UPDATE);
        audio.collect(collectAudio);
    }
    // "Lascia" gli oggetti, lo comunica alla UI e in fine avvia la clip audio
    // per fare capire che l'oggetto è stato collezionato
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


    // Entro in un collider
    public void ActivateE(Collider objectRecived)
    {
        this.interact = true;
        this.tagInteraction = objectRecived.tag;
        this.objectToDestroy = objectRecived;
        Transform transform = objectRecived.transform;
        // Gowing here...
        // Debug.Log("Entro in " + objectRecived.tag);
        Outline outline = objectRecived.GetComponent<Outline>();
        if (outline)
        {
            //var outline = objectRecived.AddComponent<Outline>();
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 5f;
        }
        else
        {
            Debug.Log("In " + objectRecived.tag + "non è presente il componente 'Outline' ");
        }

    }

    // Esco da un collider
    public void DeactivateE(Collider objectRecived)
    {

        this.interact = false;
        this.tagInteraction = "";
        Transform transform = objectRecived.transform;
        // Not glowing here...
        // Debug.Log("Esco da " + objectRecived.tag);
        Outline outline = objectRecived.GetComponent<Outline>();
        if (outline)
        {
            outline.OutlineWidth = 0f;
        }
        else
        {
            Debug.Log("In " + objectRecived.tag + " non è presente il componente 'Outline' ");
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
    // Manda un messaggio alla UI per indicare la fine della missione
    public void MissionComplete(string missionTag){
        if (GameEvent.MISSIONE_TELEVISIONI == missionTag) {
            this.tvComplete = true;
        }
            Messenger.Broadcast(missionTag);
    }

    
}
