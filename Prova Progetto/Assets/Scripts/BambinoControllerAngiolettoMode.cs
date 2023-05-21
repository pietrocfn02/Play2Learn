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
    private int[] inventary = {0,0,0,0,0}; // Parola, Clipboard, Coni, Tape, Calcolatrice,... (Da aggiungere quando colleziono)
    
    // Bool che permette di interagire
    private bool interact = false;
    
    // Controller
    private GameObject player;
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
    [SerializeField] private TMP_Text[] projectText;
    [SerializeField] private GameObject[] prefabsMission;

    // Indica lo stato delle missioni per fare in modo che non si accavvallino
    //
    private bool[] missionComplete = new bool[3];
    //Indica le missioni in corso
    //
    private bool[] missionActive = new bool[3];
    
    private bool[] interaction = new bool[3];

    private List<Vector3> cornerPosition = new List<Vector3> {    
                                                                new Vector3(19.93f, 1.556f, 19.062f),
                                                                new Vector3(18.077f, 1.556f, 19.062f), 
                                                                new Vector3(18.077f, 1.556f, 21.924f), 
                                                                new Vector3(19.924f, 1.556f, 21.924f)
                                                             };
    private GameObject[] newInstance;
    private bool exitCond = false;
    //<-----

    private int missionType = 0;
    //Indica se c'è qualche scena in cui si sta parlando
    //
    public static bool talking = false;

    private int missionDone = 0;
    private bool spawnedControl = false;
    private int contInteraction = 0;
    private int contPaper = 0;
    private Vector3 endTapePosition;
    private bool[] pushInteraction = new bool[4];
    private bool canInstantiate = true;
    private Vector3 startPosition;
    private int startPositionInt;
    private GameObject tape1;
    private bool[] arch = new bool[4]; // (0-1, 1-0), (1-2, 2-1), (2-3, 3-2), (3-0, 0-3)
    // Methods...
    void Start(){

        player = GameObject.FindWithTag(GameEvent.PLAYER_TAG);
        newInstance = new GameObject[cornerPosition.Count];
    }

    void Update(){
        //Tutorial();
        missionComplete[0] = true;
        if (interact && missionComplete[0])
        {
            switch (tagInteraction)
            {
                case GameEvent.EASEL_TAG:
                    missionType = 1;
                    break;
                case GameEvent.TRIANGLE_TAG:
                    missionType = 2;
                    break;
                default:
                    break;
            }
            StartMission(missionType);

        }
    }

    private void StartMission(int missionInt)
    {
        
        switch (missionInt)
        {
            case 1:
                Art();
                break;
            case 2:
                Math();
                break;
            default:
                break;
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

    // Disattiva il "!" 
    public void DeactivateMark()
    {
        GameObject parent = GameObject.Find(tagInteraction);
        foreach (Transform child in parent.transform)
        {
            if (child.tag == GameEvent.MARK_TAG)
            {
               child.gameObject.SetActive(false);
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


    // Fa spawnare gli oggetti per interagire con gli oggetti delle missioni
    public void SpawnMark(string fatherName)
    {
        GameObject parent = GameObject.Find(fatherName);
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
    
    // In base alla missione ricevuta come intero, vede se ci sono altre missioni attive 
    public bool InMission(int inMission)
    {
        for (int i=0; i<missionActive.Length; ++i)
        {
            if(missionActive[i] && inMission != i)
            {
                return true;
            }
        }
        return false;
    }
    public void SpawnInteraction(){
        GameObject mission = GameObject.FindWithTag(tagInteraction);
        if (mission != null)
        {

            GameObject newInstance;
            switch (tagInteraction)
            {
                case GameEvent.FLAG_TAG:
                    if (!interaction[0])
                    {
                        interaction[0] = true;
                        newInstance = Instantiate(prefabsMission[0], new Vector3(20.2f,1.7f,15f), Quaternion.identity);
                        Destroy(newInstance.GetComponent<BoxCollider>());
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
                        newInstance = Instantiate(prefabsMission[1], new Vector3(27.2f,1.7f,15.2f), Quaternion.Euler(0f, 0f, 45f));
                        Destroy(newInstance.GetComponent<BoxCollider>());
                    }
                    else
                    {
                        Debug.Log("Già attivo. Riproduco...");
                    }
                    break;

                case GameEvent.TRIANGLE_TAG:
                    if (!interaction[2])
                    {
                        interaction[2] = true;
                        newInstance = Instantiate(prefabsMission[2], new Vector3(20.3f, 1.7f, 20.46f), Quaternion.Euler(90f, 90f, 0f));
                        Destroy(newInstance.GetComponent<CalculatorEvent>());
                        Destroy(newInstance.GetComponent<BoxCollider>());
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
        if (!InMission(1))
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
                if (Input.GetKeyUp(KeyCode.E) && !missionActive[1])
                {
                    RemoveMark();
                    SpawnInteraction();
                    Messenger.Broadcast("ArtMission");
                    SetActive(1);
                    interact = false;
                    for(int i=0; i<signTag.Length; ++i)
                    {
                        SetOutline(GameObject.FindWithTag(signTag[i]), 2f,Color.yellow);
                    }
                }
                if (Input.GetKeyUp(KeyCode.E) && missionActive[1]){
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
        else
        {
            Debug.Log("Stai svolgendo già una missione. Concludi la missione già iniziata prima di passare alla prossima.");
        }
        
    }


    public void Math()
    {
        if (!InMission(2))
        {   
            if (interact)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    if (tagInteraction == GameEvent.TRIANGLE_TAG && !missionActive[2])
                    {
                        SetActive(2);
                        RemoveMark();
                        SpawnInteraction();
                        //Messenger.Broadcast("MathMission");
                        SpawnMark("Clipboard");
                        //RelativeMovement.SetInMission(true);
                    }
                    else if (missionActive[2])
                    {
                        if (tagInteraction == GameEvent.CLIPBOARD_TAG && !exitCond && inventary[1] <= 0)
                        {
                            if (contInteraction <= 1)
                            {
                                // Serve per la spiegazione
                                ++contInteraction;
                                inventary[1] = 1;
                                DeactivateMark();
                                SpawnMark("ConeContainer");
                                // RelativeMovement.SetInMission(true);
                                // Messenger.Broadcast("MathMission2");
                                SetOutline(GameObject.FindWithTag(GameEvent.CLIPBOARD_TAG), 0f, Color.yellow);
                            }
                        }
                        else if (tagInteraction == GameEvent.CONE_TAG && inventary[2] <= 0 && inventary[1] >= 1)
                        {
                            RaccoltoOggetto(2);
                            for(int i=0; i<cornerPosition.Count; ++i)
                            {
                                newInstance[i] = Instantiate(prefabsMission[4], cornerPosition[i], Quaternion.identity);
                                newInstance[i].transform.position = new Vector3(newInstance[i].transform.position.x, 1.7f, newInstance[i].transform.position.z);
                                newInstance[i].AddComponent<SphereCollider>().isTrigger = true;
                                newInstance[i].tag = GameEvent.ARROW_GENERIC+i;
                            }
                            inventary[2] = 4;
                            // Messenger.Broadcast("MathMission3");
                        }
                        else if (tagInteraction.Contains(GameEvent.ARROW_GENERIC) && inventary[2] >= 1)
                        {
                            string[] splitted = tagInteraction.Split(GameEvent.ARROW_GENERIC);
                            int pos = int.Parse(splitted[1]);
                            
                            if (!pushInteraction[pos]) 
                            {
                                Destroy(newInstance[pos]);
                                GameObject cornerCone = Instantiate(prefabsMission[3], cornerPosition[pos], Quaternion.Euler(-90f,0f,0f));
                                cornerCone.transform.position = new Vector3(cornerCone.transform.position.x, 1.5f, cornerCone.transform.position.z);
                                cornerCone.tag = GameEvent.ARROW_GENERIC + pos;
                                --inventary[2];
                                pushInteraction[pos] = true;
                                if (pos <= 1)
                                {
                                    projectText[pos].text = ")";
                                    projectText[pos].color = Color.green;
                                }
                                else
                                {
                                    projectText[pos].text = "(";
                                    projectText[pos].color = Color.green;
                                }
                            }
                            if (inventary[2] <= 0)
                            {
                                
                                // RelativeMovement.SetInMission(true);
                                // Messenger.Broadcast("MathMission4");
                                exitCond = true;
                                SpawnMark(GameEvent.TAPE_TAG);
                            }
                        }
                        else if(exitCond)
                        {
                            
                            if (tagInteraction == GameEvent.TAPE_TAG)
                            {
                                inventary[3] = 1;
                                Destroy(GameObject.FindWithTag(GameEvent.TAPE_TAG));
                                // Messenger.Broadcast("MathMission5");
                            }
                            else if (tagInteraction.Contains(GameEvent.ARROW_GENERIC) && inventary[3] >= 1 && contPaper < 4)
                            {
                                string[] splitted = tagInteraction.Split(GameEvent.ARROW_GENERIC);
                                int pos = int.Parse(splitted[1]);
                                if(canInstantiate)
                                {
                                    // Spawn del nastro
                                    TapeMovement.SetTapeMove(true);
                                    TapeMovement.SetStartPosition(cornerPosition[pos]);
                                    startPosition = cornerPosition[pos];
                                    startPositionInt = pos;
                                    tape1 = Instantiate(prefabsMission[5], startPosition, Quaternion.identity);
                                    canInstantiate = false;
                                }
                                
                                if((startPositionInt == 0 && pos == 1) && (!arch[0]))
                                {
                                    TapeMovement.SetFinalPosition(cornerPosition[0]);
                                    TapeMovement.SetTapeMove(false);
                                    GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(141f,0f,180f));
                                    tape2.transform.localScale = new Vector3(1.85f,0.03f,0.03f);
                                    Destroy(tape1);
                                    ++contPaper;
                                    canInstantiate = true;
                                    arch[0] = true;
                                    projectText[4].text = "1.85 m.";
                                    projectText[8].text = "-----------";
                                    projectText[8].color = Color.green;
                                    
                                }
                                else if((startPositionInt == 1 && pos == 0) && (!arch[0]))
                                {
                                    TapeMovement.SetFinalPosition(cornerPosition[0]);
                                    TapeMovement.SetTapeMove(false);
                                    GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(141f,0f,0f));
                                    tape2.transform.localScale = new Vector3(1.85f,0.03f,0.03f);
                                    Destroy(tape1);
                                    ++contPaper;
                                    canInstantiate = true;
                                    arch[0] = true;
                                    projectText[4].text = "1.85 m.";
                                    projectText[8].text = "-----------";
                                    projectText[8].color = Color.green;
                                }
                                else if((startPositionInt == 1 && pos == 2) && (!arch[1]))
                                {
                                    TapeMovement.SetFinalPosition(cornerPosition[2]);
                                    TapeMovement.SetTapeMove(false);
                                    GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(219f,-90f,0f));
                                    tape2.transform.localScale = new Vector3(2.9f,0.03f,0.03f);
                                    Destroy(tape1);
                                    ++contPaper;
                                    canInstantiate = true;
                                    arch[1] = true;
                                    projectText[5].text = "2.9 m.";
                                    projectText[9].text = "-------------";
                                    projectText[9].color = Color.green;
                                }
                                else if((startPositionInt == 2 && pos == 1) && (!arch[1]))
                                {
                                    TapeMovement.SetFinalPosition(cornerPosition[2]);
                                    TapeMovement.SetTapeMove(false);
                                    GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(219f,-90f,180f));
                                    tape2.transform.localScale = new Vector3(2.9f,0.03f,0.03f);
                                    Destroy(tape1);
                                    ++contPaper;
                                    canInstantiate = true; 
                                    arch[1] = true;
                                    projectText[5].text = "2.9 m.";
                                    projectText[9].text = "-------------";
                                    projectText[9].color = Color.green;
                                    
                                }
                                else if((startPositionInt == 2 && pos == 3) && (!arch[2]))
                                {
                                    TapeMovement.SetFinalPosition(cornerPosition[0]);
                                    TapeMovement.SetTapeMove(false);
                                    GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(219f,0f,0f));
                                    tape2.transform.localScale = new Vector3(1.85f,0.03f,0.03f);
                                    Destroy(tape1);
                                    ++contPaper;
                                    canInstantiate = true;
                                    arch[2] = true;
                                    projectText[6].text = "1.85 m.";
                                    projectText[10].text = "-----------";
                                    projectText[10].color = Color.green;
                                }
                                else if((startPositionInt == 3 && pos == 2) && (!arch[2]))
                                {
                                    TapeMovement.SetFinalPosition(cornerPosition[0]);
                                    TapeMovement.SetTapeMove(false);
                                    GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(219f,0f,180f));
                                    tape2.transform.localScale = new Vector3(1.85f,0.03f,0.03f);
                                    Destroy(tape1);
                                    ++contPaper;
                                    canInstantiate = true;
                                    arch[2] = true;
                                    projectText[6].text = "1.85 m.";
                                    projectText[10].text = "-----------";
                                    projectText[10].color = Color.green;
                                }
                                else if((startPositionInt == 3 && pos == 0) && (!arch[3]))
                                {
                                    TapeMovement.SetFinalPosition(cornerPosition[2]);
                                    TapeMovement.SetTapeMove(false);
                                    GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(141f,-90f,180f));
                                    tape2.transform.localScale = new Vector3(2.9f,0.03f,0.03f);
                                    Destroy(tape1);
                                    ++contPaper;
                                    canInstantiate = true; 
                                    arch[3] = true;
                                    projectText[7].text = "2.9 m.";
                                    projectText[11].text = "-------------";
                                    projectText[11].color = Color.green;
                                    Debug.Log("Sono in 3-0");
                                }
                                else if((startPositionInt == 0 && pos == 3) && (!arch[3]))
                                {
                                    TapeMovement.SetFinalPosition(cornerPosition[2]);
                                    TapeMovement.SetTapeMove(false);
                                    GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(141f,-90f,0f));
                                    tape2.transform.localScale = new Vector3(2.9f,0.03f,0.03f);
                                    Destroy(tape1);
                                    ++contPaper;
                                    canInstantiate = true;
                                    arch[3] = true; 
                                    projectText[7].text = "2.9 m.";
                                    projectText[11].text = "-------------";
                                    projectText[11].color = Color.green;
                                    Debug.Log("Sono in 0-3");
                                }
                            }
                            if (contPaper >= 4 && inventary[3] >= 1)
                            {
                                // Messenger.Broadcast("MathMission6");
                                // RelativeMovement.SetInMission(true);
                                SpawnMark(GameEvent.CALC_TAG);
                                inventary[3] = 0;
                            }
                            else if (tagInteraction == GameEvent.CALC_TAG && inventary[4] <= 0)
                            {
                                RaccoltoOggetto(4);
                                // Messenger.Broadcast("MathMission7");
                                // SpawnMark(GameEvent.CLIPBOARD_TAG);
                            }
                            else if (tagInteraction == GameEvent.CLIPBOARD_TAG && inventary[4] >= 1)
                            {
                                RemoveMark();
                                RelativeMovement.SetInMission(true);
                                Messenger.Broadcast("UIMathMission");
                                Instantiate(prefabsMission[2], new Vector3(18.665f,1.613f,21.048f), Quaternion.Euler(-66.7f,0f,0f));
                                player.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("Stai svolgendo già una missione. Concludi la missione già iniziata prima di passare alla prossima.");
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
        if (InMission(1))
        {
            Debug.Log(missionActive[1]);
            Debug.Log("Missione Arte Completa");
            Debug.Log("Cerca in giro per la mappa gli interagibili relativi all'arte");
            SetComplete(1);
            missionType = 0;
        }
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