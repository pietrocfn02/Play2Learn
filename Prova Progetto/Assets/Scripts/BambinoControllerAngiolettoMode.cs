using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//to do something
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoControllerAngiolettoMode : MonoBehaviour 
{
    
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
    private bool[] missionComplete = new bool[5];
    //Indica le missioni in corso
    //
    private bool[] missionActive = new bool[5];
    
    private bool[] interaction = new bool[5];
    //<-----
    

    bool tab = false;
    //Indica se c'è qualche scena in cui si sta parlando
    //
    bool talking = false;
    // indica se c'è un oggetto interagibile
    //
    bool explanationDone = false;

    private bool missionDone = false;

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
                missionActive[mission] = true;            
            }
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


    // In base al numero specificato riproduce la UI relativa all'interagibile
    public void ReproduceExplanation(int interactable){
        if (!interaction[interactable])
        {
            interaction[interactable] = true;
            //Messenger.Broadcast(RiproduciExplanation, interactable);
            
            Debug.Log("Interazione (false) " + tagInteraction);
            // Spawn dell'interagibile piccolo
            RemoveMark("Flag");
            GameObject mission = GameObject.FindWithTag(tagInteraction);
            Instantiate(prefabs[0], new Vector3(20.2f,1.7f,15f), Quaternion.identity);
        }
        else
        {
            Debug.Log("Interazione (true) " + tagInteraction);
        }
    }
    // Inizia il tutorial
    //
    public void Tutorial()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && !missionActive[0])
        {
            Debug.Log("Spiegazione movimenti");
            SpawnMission("Flag");
        }
        if (interact && !talking)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                interact = false;
                if (tagInteraction == "Flag" )
                {   
                    
                    ReproduceExplanation(0); // Riproduce la spiegazione
                    //Mandare un broadcast per dire che spawna la missione
                    //Quindi è finita la spiegazione nella UI
                    SpawnMission("TutorialRoom");
                    Debug.Log(tagInteraction);
                }
                else if (tagInteraction == GameEvent.MARK_TAG)
                {
                    RaccoltoOggetto(0);
                    if (inventary[0] >= 10)
                    {
                        SpawnMission("Table");
                        Debug.Log("Vai verso il tavolo e interagisci");
                    }
                }
                else if (tagInteraction == GameEvent.TABLE_TAG && !missionComplete[0] && missionActive[0])
                {
                    if(inventary[0] >= 10)
                    {
                        LasciaOggetto(0);
                        RemoveMark(GameEvent.TABLE_TAG);
                        Debug.Log("Inizio Missione");
                        missionComplete[0] = true;
                    }
                    else
                    {
                        Debug.Log("Devi raccogliere tutto");
                    }
                }
            }
            if (missionComplete[0] && missionActive[0])
            {
                Debug.Log("Missione completata !!");
                missionActive[0] = false;
            }
        }
    }
    // Per impedire che si facciano delle azioni senza i collezionabili necessari.
    // Appare un messaggio UI che ci avvisa
    public void notNow(){
        Debug.Log("Adesso non puoi !!");
        //Messenger.Broadcast(GameEvent.FORGET);
    }
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

