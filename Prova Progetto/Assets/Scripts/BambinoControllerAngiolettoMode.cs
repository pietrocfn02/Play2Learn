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


    // -----> Gestiscono le missioni
    // Prefabs per gli interagibili
    //
    [SerializeField] private GameObject[] prefabsMission; // La posizione 0 è occupata dalla bandiera

    // Indica lo stato delle missioni per fare in modo che non si accavvallino
    //
    private bool[] missionComplete = new bool[3];
    //Indica le missioni in corso
    //
    private bool[] missionActive = new bool[3];
    
    private bool[] interaction = new bool[3];
    //<-----


    private bool interactTmp = false;
    //Indica se c'è qualche scena in cui si sta parlando
    //
    public static bool talking = false;

    private int missionDone = 0;
    private bool spawnedControl = false;

    // Methods...
    void Start(){}

    void Update(){
        //Tutorial();
        if (interact)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                switch (tagInteraction)
                {
                    case GameEvent.EASEL_TAG:
                        Art();
                        break;
                    case "Triangle":
                        Math();
                    case default:
                        break;
                }
            }
        }
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
    public void RemoveMark()
    {
        GameObject parent = GameObject.Find(tagInteraction);
        foreach (Transform child in parent.transform)
        {
            if (child.tag == GameEvent.MARK_TAG)
            {
                Destroy(child.gameObject);
            }
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
    public void SpawnMark(string tagFather)
    {
        GameObject parent = GameObject.Find(tagFather);
        Debug.Log(parent);
        if (parent)
        {
            foreach(Transform child in parent.transform)
            {
                if (child)
                {
                    if(child.tag == GameEvent.MARK_TAG)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void SpawnTutorialMission()
    {
        GameObject parent = GameObject.Find("TutorialRoom");
        Transform child = parent.GetComponent<Transform>().GetChild(0);
        Debug.Log(child);
        if (child)
        {
            child.gameObject.SetActive(true);
        }
    }
    
    // In base al numero specificato riproduce la UI relativa all'interagibile
    // Cambiare in base al tag ricevuto
    
    public void SpawnInteraction(){
        GameObject mission = GameObject.FindWithTag(tagInteraction);
        if (mission != null)
        {
            switch (tagInteraction)
            {
                case GameEvent.FLAG_TAG:
                    if (!interaction[0])
                    {
                        interaction[0] = true;
                        Instantiate(prefabsMission[0], new Vector3(20.2f,1.7f,15f), Quaternion.identity);
                    }
                    else
                    {
                        Debug.Log("Già attivo. Riproduco...");
                    }
                    break;
            
                case GameEvent.EASEL_TAG:
                    if (!interaction[1])
                    {
                        interaction[1] = true;
                        Instantiate(prefabsMission[1], new Vector3(27.2f,1.7f,15.2f), Quaternion.Euler(0f, 0f, 45f));
                    
                    }
                    else
                    {
                        Debug.Log("Già attivo. Riproduco...");
                    }
                    break;

            
                default:
                    break;
            }
        }
    }
    
    // Controlla se c'è qualche missione attiva
    // Se c'è anche solo una missione attiva return false
    // true altrimenti
    private bool ActiveControl()
    {
        for(int i=0; i<missionActive.Length; ++i)
        {
            if (missionActive[i])
            {
                return false;
            }
        }
        return true;
    }
    public void Tutorial()
    {
        
        if (Input.GetKeyUp(KeyCode.Tab) && !missionActive[0])
        {
            RelativeMovement.SetInMission(true);
            SpawnMark(GameEvent.FLAG_TAG);
            Messenger.Broadcast("MarkExplanation");
            SetActive(0);
        }

        if (interact && missionActive[0])
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                interact = false;
                if (tagInteraction == GameEvent.FLAG_TAG && !spawnedControl)
                {   
                    RelativeMovement.SetInMission(true);
                    SpawnInteraction(); // Riproduce la spiegazione
                    //Mandare un broadcast per dire che spawna la missione
                    //Quindi è finita la spiegazione nella UI
                    RemoveMark();
                    SpawnTutorialMission();
                    Messenger.Broadcast("PrefabExplanation");
                    spawnedControl = true;
                }
                else if (tagInteraction == GameEvent.MARK_TAG)
                {
                    RaccoltoOggetto(0);
                    if (inventary[0] >= 10)
                    {
                        SpawnMark(GameEvent.TABLE_TAG);
                        Messenger.Broadcast("TableInteraction");
                    }
                }
                else if (tagInteraction == GameEvent.TABLE_TAG && !missionComplete[0] && missionActive[0])
                {
                    if(inventary[0] >= 10)
                    {
                        LasciaOggetto(0);
                        RemoveMark();
                        Messenger.Broadcast(GameEvent.FIRST_UI_MISSION);
                        
                    }
                    else
                    {
                        Messenger.Broadcast("ErrorInteraction");
                    }
                }
            }
            if (missionComplete[0] && missionActive[0])
            {
                RelativeMovement.SetInMission(true);
                Messenger.Broadcast("EndTutorial");
                missionActive[0] = false;
                // Attivare gli altri interagibili relativi alle missioni di italiano
                Messenger.Broadcast("MissionComplete");
                //SpawnAll(materia);
                // Attivare gli interagibili relativi alle altre missioni
            }
        }
    }
    
    public void Art()
    {
        if (ActiveControl())
        {
            string[] signTag = {GameEvent.VITRUVIAN_TAG, 
                                GameEvent.COLUMN_CORINTHIAN_TAG,
                                GameEvent.COLUMN_IONIC_TAG,
                                GameEvent.TOPOLINO_TAG,
                                GameEvent.ONEPIECE_TAG,
                                GameEvent.SNOOPY_TAG,
                                GameEvent.SUPERMAN_TAG
                                };

            if (interact)
            {
                if (Input.GetKeyUp(KeyCode.E) && !interactTmp)
                {
                    RemoveMark();
                    SpawnInteraction();
                    Messenger.Broadcast("ArtMission");
                    for(int i=0; i<signTag.Length; ++i)
                    {
                        SetOutline(GameObject.FindWithTag(signTag[i]), 2f,Color.yellow);
                    }
                    interactTmp = true;
                }else if (Input.GetKeyUp(KeyCode.E) && interactTmp){

                switch(tagInteraction)
                    {
                        case GameEvent.VITRUVIAN_TAG:
                            Messenger.Broadcast(GameEvent.VITRUVIAN_TAG);
                            break;
                        case GameEvent.COLUMN_CORINTHIAN_TAG:
                            Messenger.Broadcast(GameEvent.COLUMN_CORINTHIAN_TAG);
                            break;
                        case GameEvent.COLUMN_IONIC_TAG:
                            Messenger.Broadcast(GameEvent.COLUMN_IONIC_TAG);
                            break;
                        case GameEvent.TOPOLINO_TAG:
                            Messenger.Broadcast(GameEvent.TOPOLINO_TAG);
                            break;
                        case GameEvent.ONEPIECE_TAG:
                            Messenger.Broadcast(GameEvent.ONEPIECE_TAG);
                            break;
                        case GameEvent.SNOOPY_TAG:
                            Messenger.Broadcast(GameEvent.SNOOPY_TAG);
                            break;
                        case GameEvent.SUPERMAN_TAG:
                            Messenger.Broadcast(GameEvent.SUPERMAN_TAG);
                            break;
                        
                        default:
                            break;    
                    }
                }
            }
        }
    }


    public void Math()
    {
        if (ActiveControl())
        {

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


    public void SetOutline(GameObject objectRecived, float power, Color color)
    {
        Outline outline = objectRecived.GetComponent<Outline>();
        if (outline)
        {
            outline.OutlineColor = color;
            outline.OutlineWidth = power;
        }
    }

    // Entro in un collider
    public void ActivateE(Collider objectRecived)
    {
        this.interact = true;
        this.tagInteraction = objectRecived.tag;
        this.objectToDestroy = objectRecived;
        SetOutline(objectRecived.gameObject, 4, Color.yellow);
    }


    // Esco da un collider
    public void DeactivateE(Collider objectRecived)
    {

        this.interact = false;
        this.tagInteraction = "";
        SetOutline(objectRecived.gameObject, 0, Color.yellow);
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

    public void MissionTutorialDone()
    {
        ++missionDone;
        if (missionDone >= 10)
        {
            SetComplete(0);
            GameObject mission = GameObject.FindWithTag("Tutorial");
            Destroy(mission.GetComponent<Outline>());
            Messenger.Broadcast("EndItalian");
            SpawnMark(GameEvent.EASEL_TAG);
            //SpawnMark(GameEvent.EASEL_TAG);
            Debug.Log("Spawn");
        }
    }
    
    public void MissionArtDone()
    {
        SetComplete(1);
        Debug.Log("Missione Arte Completa");
        Debug.Log("Cerca in giro per la mappa gli interagibili relativi all'arte");
    }

    void Awake()
    {
        Messenger.AddListener("MissionTutorialDone", MissionTutorialDone);
        Messenger.AddListener("MissionArtDone", MissionArtDone);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener("MissionTutorialDone", MissionTutorialDone);
        Messenger.RemoveListener("MissionArtDone", MissionArtDone);
    }

}

