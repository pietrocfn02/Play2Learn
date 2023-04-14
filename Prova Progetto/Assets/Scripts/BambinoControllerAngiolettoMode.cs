using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//to do something
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoControllerAngiolettoMode : MonoBehaviour {
    
    // Variables... 

    // Controller per azioni della "Angioletto_Scene"
    private int initialSize = 0;
    private int angioletto_score = 0;

    // Array per l'inventario
    private int[] inventary = {0,0,0}; // Parola, Sparrow, Telecomando, ... (Da aggiungere quando colleziono)
    
    // Bool che permette di interagire
    private bool interact = false;
    
    // Controller
    private CharacterController _charController;
    //Tag dell' oggetto colpito
    private string tagInteraction = "";
    // Prefab
    [SerializeField] private GameObject goodCoinPrefab;
    // Material per spegnere la tv
    [SerializeField] private Material newMaterial;

    // Audio...
    // Spostare
    [SerializeField] public AudioClip clipTv;
    [SerializeField] public AudioClip pemAudio;
    [SerializeField] public AudioClip collectAudio;
    [SerializeField] public AudioClip collectCoinAudio;
    [SerializeField] public AudioClip releseAudio;
    // I coin
    //
    private GameObject[] _coins;
    // Le TV
    //
    private GameObject[] tv;

    // Collider dell'oggetto da distruggere
    //
    private Collider objectToDestroy;
    // Indica le TV spente
    //
    private int count = 0;
    // gestisce l'audio (Da spostare ?)
    //
    private AudioManager audio = new AudioManager();
    // indica il completamento della missione delle Tv
    //
    private bool tvComplete = false;

    
    // Add...

    // -----> Gestiscono le missioni
    // Prefabs per gli interagibili
    //
    [SerializeField] private GameObject[] prefabs; // La posizione 0 è occupata dalla bandiera

    // Indica lo stato delle missioni per fare in modo che non si accavvallino
    //
    private bool[] missionComplete = new bool[14];
    //Indica le missioni in corso
    //
    private bool[] missionActive = new bool[14];
    // Indica le missioni 
    //
    private string[] mission = { };
    // Indica il tutorial
    //
    //<-----
    

    bool tab = false;
    //Indica se c'è qualche scena in cui si sta parlando
    //
    bool talking = false;
    // indica se c'è un oggetto interagibile
    //
    bool interactableObj = false;

    private bool homeworkDone = false;

    // Methods...
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

    // Broadcast nella classe UI
    //
    IEnumerator TutorialMovementCollections()
    {
        talking = true;
        Debug.Log("Benvenuto nel tutorial...");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Iniziamo a vedere come muoversi.");
        yield return new WaitForSeconds(2);
        Debug.Log("Per prima cosa usa i comandi ( W,A,S,D, [SPACE] ) per muoverti e saltare.");
        yield return new WaitForSeconds(2);
        Debug.Log("Vai verso [oggetto illuminato].");
        yield return new WaitForSeconds(2);
        Debug.Log("Quando vedrai un oggetto illuminarsi significa che potrai interagire con esso!");
        yield return new WaitForSeconds(2);
        Debug.Log("Prova a premere E e collezionarlo!");
        yield return new WaitForSeconds(2);
        talking = false;
    }

    // Broadcast nella classe UI
    //
    IEnumerator TutorialMission()
    {
        talking = true;
        Debug.Log("Bene !!!");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Adesso trova tutte le parole e portale al tavolo per ricostruire la frase...");
        yield return new WaitForSeconds(2);
        Debug.Log("In seguito vai verso il tavolo per completare la prima missione...");
        // Vedemmo uno stormo di uccelli che migrava verso terre lontane
        yield return new WaitForSeconds(2);
        talking = false;
    }

    // Broadcast nella classe UI
    //
    IEnumerator TutorialInteractable1()
    {
        talking = true;
        Debug.Log("Fantastico !!!");
        yield return new WaitForSeconds(2);
        Debug.Log("Voglio complimentarmi con te...");
        yield return new WaitForSeconds(2);
        Debug.Log("Ora che hai finito vai verso il punto interrogativo (?)");
        yield return new WaitForSeconds(2);
        interactableObj = false;
        talking = false;
    }

    // Broadcast nella classe UI
    //
    IEnumerator TutorialInteractable2()
    {
        talking = true;
        // Trasformare l'interagibile nel modello 3D inerente
        Debug.Log("Questo è uno degli interagibili della mappa...");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Ogni volta che completi una missione, sbloccherai un interagibile.");
        yield return new WaitForSeconds(2);
        Debug.Log("Come ultimo compito, prova ad avvicinarti e premere E...");
        yield return new WaitForSeconds(2);
        talking = false;
    }

    // Broadcast nella classe UI
    //
    IEnumerator EndTutorial()
    {
        
        talking = true;
        Debug.Log("Ottimo !!!");
        yield return new WaitForSeconds(2);
        Debug.Log("Quello che hai appena visto è la storia della bandiera italiana.");
        yield return new WaitForSeconds(2);
        Debug.Log("Come puoi vedere il simbolo al di sotto la bandiera è cambiato...");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Ricorda...");
        yield return new WaitForSeconds(2);
        Debug.Log("Ogni interagibile ha una storia personale...");
        yield return new WaitForSeconds(2);
        Debug.Log("Divertiti a scoprire tutti gli interagibili della mappa...");
        yield return new WaitForSeconds(2);
        Debug.Log("Ogni volta che vorrai, potrai avere a che fare con loro.");
        yield return new WaitForSeconds(2);
        Debug.Log("Dovrai soltanto avvicinarti ai propi simboli fluttuanti !!!");
        yield return new WaitForSeconds(2);
        Debug.Log("Complimenti hai completato il tutorial !!!");
        yield return new WaitForSeconds(2);
        talking = false;
    }
    void Update(){
            Tutorial();
    }

    // Setta una missione su "Attivo"
    public void SetActive(int mission)
    {
        if (!missionComplete[mission])
        {
            if (!missionActive[mission])
            {
                missionActive[mission] = true;            }
            else
            {
                Debug.Log("Missione già attiva");
            }
        }
        else
        {
            Debug.Log("Missione già eseguita");
        }
    }

    // Imposta su attivo il componente che stiamo cercando. Inoltre setta l'outline su un valore
    // Potrei mettere qui il controllo sul tipo (se interagibile o se una missione) N.D.A.
    // Riceve come parametri il tag del oggetto padre (tagFather) e un valore, float, dell'outline (value)
    public void SetOutline(string tagFather, float value)
    {

        GameObject toModify = GameObject.FindWithTag(tagFather);
        Outline outline = toModify.GetComponent<Outline>();
        if (outline)
        {
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = value;
            Transform tmpChild = toModify.GetComponent<Transform>().GetChild(0);
            tmpChild.gameObject.SetActive(true);
            Outline outlineChild = tmpChild.GetComponent<Outline>();
            outlineChild.OutlineColor = Color.yellow;
            outlineChild.OutlineWidth = value;
        }
        else
        {
            Debug.Log("Non ha l'outline");
        }
        
    }
    // Elimina il "!" -- viene usato alla fine di una missione --
    public void RemoveMark(string tagFather)
    {
        GameObject toModify = GameObject.FindWithTag(tagFather);
        Transform tmpChild = toModify.GetComponent<Transform>().GetChild(0);
        if (tmpChild)
        {
            Destroy(tmpChild.gameObject);
            Debug.Log("KABOOOOOOOM." + tmpChild.tag + " è stato distrutto");
        }
    }

    // Setta una missione su "Completa"
    public void SetComplete(int mission)
    {
        if (!missionComplete[mission])
        {
            if (missionActive[mission])
            {
                missionActive[mission] = false;
                missionComplete[mission] = true;
            }
            else
            {
                Debug.Log("La missione non è attiva");
            }
        }
        else
        {
            Debug.Log("La missione non è completa");
        }
    }


    // Fa spawnare gli oggetti relativi alle missioni
    public void SpawnMission(string tagMission)
    {
        GameObject mission = GameObject.Find(tagMission);
        Transform tmpChild = mission.GetComponent<Transform>().GetChild(0);
        SetActive(0);
        tmpChild.gameObject.SetActive(true);
    }


    
    // Controlla la lista delle missioni finite e assegna ad ogni interagibile il propio simbolo
    // ES. bandiera alla bandiera 
    public void SetInteraction()
    {

    }
    // Inizia il tutorial
    //
    public void Tutorial()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && !missionActive[0])
        {
            // Avvia le prime frasi
            SpawnMission("TutorialRoom");
            
            Debug.Log("Premo [TAB]");
            //StartCoroutine(TutorialMovementCollections());
        }
        if (interact && !talking)
        {
            if (Input.GetKeyUp(KeyCode.E) && missionActive[0])
            {
                interact = false;
                //WORDS_TAG
                if (tagInteraction == GameEvent.MARK_TAG) 
                { 
                    RaccoltoOggetto(0);
                    //StartCoroutine(TutorialMission());
                    if (inventary[0] >=  10)
                    {
                        SetOutline(GameEvent.TABLE_TAG, 5);
                    }
                
                }
                else if (tagInteraction == GameEvent.TABLE_TAG) 
                {
                    if (!talking && inventary[0] >= 10)
                    {
                        RemoveMark(GameEvent.TABLE_TAG);
                        Messenger.Broadcast(GameEvent.FIRST_UI_MISSION);
                        Debug.Log("Compongo la frase " + inventary[0]);
                        LasciaOggetto(0);
                        // Fare partire una coroutine per formare la scritta
                        interactableObj = true;
                        Debug.Log("Ho finito i compiti " + inventary[0]);
                        SetOutline(GameEvent.INTERACTIVE_TAG, 5);
                    }
                    else
                        notNow();
                }
                else if (tagInteraction == GameEvent.INTERACTIVE_TAG && inventary[0] <= 0 && interactableObj)
                {
                    //StartCoroutine(TutorialInteractable1());
                    //SetInterac(fatherTag);
                    Debug.Log("Question Mark");
                    interactableObj = false; // Rende l'oggetto "istruttivo" interagibile
                    homeworkDone = true; // Va settata su true quando finiamo la missione dei compiti (Broadcast???)
                }
                else if (!talking && tagInteraction == GameEvent.INTERACTIVE_TAG && homeworkDone)
                {

                    //StartCoroutine(TutorialInteractable2());
                    /* Controllare quando entra nel trigger e bloccare il gioco per la spiegazione?*/
                    Debug.Log("Quasi finito tutroial");
                    if (!talking)
                    {
                        Debug.Log("UI spiegazione");
                        // Parte la UI per la spiegazione del oggetto sbloccato
                        SetComplete(0);
                    }
                    else
                    {
                        notNow();
                    }
                }
                else
                {
                    notNow();
                }
            }
        }

    }
    // Per impedire che si facciano delle azioni senza i collezionabili necessari.
    // Appare un messaggio UI che ci avvisa
    public void notNow(){
        Debug.Log("Adesso non puoi !!");
        //Messenger.Broadcast(GameEvent.FORGET);
    }
    /*public void doHomework(){
        // Fermo il tempo per evitare che la bambina scappi mentre fa i compiti :)
        //Time.timeScale = 0;
        CompitiConnector.siamoInModalitaCompiti = true;
        //Messenger.Broadcast(GameEvent.MISSIONE_COMPITI);
        Debug.Log("Sto facendo i compiti..." + inventary[0]);
        LasciaOggetto(0);
        interactableObj = true;
        Debug.Log("Ho finito i compiti" + inventary[0]);
    }*/

    // Metodo che cambia il materiale della tv su di cui è stato chiamato OnTriggerEnter
    public void TurnOffTV(string tag){
       /* tv = GameObject.FindGameObjectsWithTag(tag);
        tv[0].GetComponent<Renderer>().material = newMaterial;
        count ++;
        GetComponent<AudioSource>().turnOffTV(tag,clipTv);
    */}
    // Aggiorna il punteggio e lo comunica alla UI
    public void UpdateAngioletto(int i) {
      /*  GetComponent<AudioSource>().releaseObject(collectCoinAudio);
        angioletto_score+=i;
        Messenger.Broadcast(GameEvent.ANGIOLETTO_UPDATE);
    */}
    // "Raccolgie" l'oggetto, lo comunica alla UI e in fine avvia la clip audio
    // per fare capire che l'oggetto è stato collezionato
    public void RaccoltoOggetto(int i){
        inventary[i] += 1;
        if(objectToDestroy!= null){
            Transform childText = objectToDestroy.gameObject.GetComponent<Transform>();
            Destroy(objectToDestroy.gameObject);
        }
        //Messenger.Broadcast(GameEvent.RACCOLTA_UPDATE);
        //audio.collect(collectAudio);
    }
    // "Lascia" gli oggetti, lo comunica alla UI e in fine avvia la clip audio
    // per fare capire che l'oggetto è stato collezionato
    public void LasciaOggetto(int i){
        if(inventary[i]>0){
            inventary[i]=0;
        }
        //Messenger.Broadcast(GameEvent.LANCIA_OGGETTO);
    }

    public int getOggettoCount(int i){
        return inventary[i];
    }

    public int getAngiolettoScore() {
        return angioletto_score;
    }

    /*public int getDiavolettoScore() {
        return diavoletto_score;
    }*/


    // Entro in un collider
    public void ActivateE(Collider objectRecived)
    {
        this.interact = true;
        this.tagInteraction = objectRecived.tag;
        this.objectToDestroy = objectRecived;
        Transform transform = objectRecived.transform;
        // Gowing here...
        Outline outline = objectRecived.GetComponent<Outline>();
        if (outline)
        {
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 5f;
        }

        

        
    }


    // Esco da un collider
    public void DeactivateE(Collider objectRecived)
    {

        this.interact = false;
        this.tagInteraction = "";
        Transform transform = objectRecived.transform;
        // Not glowing here...
        Outline outline = objectRecived.GetComponent<Outline>();
        if (outline)
        {
            outline.OutlineWidth = 0f;
        }
        Transform markChild = transform.Find(GameEvent.MARK_TAG);
        
        
    }


    // La size "determina" il numero di numeri casuali (da 1 a 10) la cui somma sara il numero di monete spawnate
    // Giusto per rendere piu proporzionale il numero di monete spawnate
    // 
    // Mantenere o Eliminare lo spawn dei coin ?
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
    /*public void MissionComplete(string missionTag){
        if (GameEvent.MISSIONE_TELEVISIONI == missionTag) {
            this.tvComplete = true;
        }
            Messenger.Broadcast(missionTag);
    }

    */
    }
