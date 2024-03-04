using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//to do something
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/BambinoController")]

public class BambinoControllerAngiolettoMode : MonoBehaviour 
{
    
    // Array per l'inventario
    private int[] inventary = {0,0,0,0,0}; // Parola, Clipboard, Coni, Tape, Calcolatrice,... (Da aggiungere quando colleziono)
    
    // Bool che permette di interagire
    private bool interact = false;
    
    // Controller
    private GameObject player;
    //Tag dell' oggetto colpito
    private string tagInteraction = "";


    // Collider dell'oggetto da distruggere
    //
    private Collider objectToDestroy;

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
    private bool[] arch = new bool[4];
    private GameObject _camera;
    // Methods...
    void Start()
    {
        player = GameObject.FindWithTag(GameEvent.PLAYER_TAG);
        _camera = GameObject.Find("Camera");
        var mathMission = _camera.GetComponent<MathMission>();
        mathMission.enabled = false;
        newInstance = new GameObject[cornerPosition.Count];
    }

    void Update(){
        Tutorial();
        //missionComplete[0] = true;

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
                case GameEvent.COLLECTABLE_TAG:
                    if (interact)
                    {
                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            if (objectToDestroy != null)
                            {
                                BroadcastMessage("AddToCollection", objectToDestroy);
                            }
                        }
                    }
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
                for (int i=0 ;i<missionActive.Length; ++i)
                {
                    missionActive[i] = false;
                }
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
                Debug.Log("Sono nella missione " + i);
                return true;
            }
        }
        return false;
    }
    public void SpawnExplenation(){
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
                        newInstance.name = "Flag";
                        //Destroy(newInstance.GetComponent<BoxCollider>());
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
                        newInstance.name = "Easel";
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
                        newInstance.name = "Triangle";
                        Destroy(newInstance.GetComponent<MathMission>());
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
        
        if (Input.GetKeyUp(KeyCode.Return) && !missionActive[0])
        {
            
            RelativeMovement.StopMovement(true);
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
                    RelativeMovement.StopMovement(true);
                    SpawnExplenation();
                    RemoveMark();
                    SpawnTutorialMission();
                    Messenger.Broadcast("PrefabExplanation");
                    spawnedControl = true;
                    GameObject mission = GameObject.FindWithTag("Flag");
                    Destroy(mission.GetComponent<BoxCollider>());
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
                RelativeMovement.StopMovement(true);
                Messenger.Broadcast("EndTutorial");
                missionActive[0] = false;
                Messenger.Broadcast("MissionComplete");
            }
        }
    }
    // Art mission
    public void Art()
    {
        if (!InMission(1))
        {
            string[] signTag = {    GameEvent.SIGN1_TAG, 
                                    GameEvent.SIGN2_TAG,
                                    GameEvent.SIGN3_TAG,
                                    GameEvent.SIGN4_TAG,
                                    GameEvent.SIGN5_TAG,
                                    GameEvent.SIGN6_TAG,
                                    GameEvent.SIGN7_TAG
                                };

            if (interact)
            {
                if (Input.GetKeyUp(KeyCode.E) && !missionActive[1])
                {
                    RemoveMark();
                    SpawnExplenation();
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
                        case GameEvent.SIGN1_TAG:
                            Messenger.Broadcast(GameEvent.SIGN1_TAG);
                            break;
                        case GameEvent.SIGN2_TAG:
                            Messenger.Broadcast(GameEvent.SIGN2_TAG);
                            break;
                        case GameEvent.SIGN3_TAG:
                            Messenger.Broadcast(GameEvent.SIGN3_TAG);
                            break;
                        case GameEvent.SIGN4_TAG:
                            Messenger.Broadcast(GameEvent.SIGN4_TAG);
                            break;
                        case GameEvent.SIGN5_TAG:
                            Messenger.Broadcast(GameEvent.SIGN5_TAG);
                            break;
                        case GameEvent.SIGN6_TAG:
                            Messenger.Broadcast(GameEvent.SIGN6_TAG);
                            break;
                        case GameEvent.SIGN7_TAG:
                            Messenger.Broadcast(GameEvent.SIGN7_TAG);
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

    // MAth mission
    private void CreateTape(int corner,Vector3 startPosition, float eulerX, float eulerY, 
                            float eulerZ, float tapeX, int archInt, int project1, int project2, string mt, string line)
    {
        TapeMovement.SetFinalPosition(cornerPosition[corner]);
        TapeMovement.SetTapeMove(false);
        GameObject tape2 = Instantiate(prefabsMission[5], startPosition, Quaternion.Euler(eulerX,eulerY,eulerZ));
        tape2.transform.localScale = new Vector3(tapeX,0.03f,0.03f);
        Destroy(tape1);
        ++contPaper;
        canInstantiate = true;
        arch[archInt] = true;
        projectText[project1].text = mt;
        projectText[project2].text = line;
        projectText[project2].color = Color.green;
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
                        Debug.Log("Inizio");
                        SetActive(2);
                        RemoveMark();
                        SpawnExplenation();
                        Messenger.Broadcast("MathMission");
                        SpawnMark("Clipboard");
                        RelativeMovement.StopMovement(true);
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
                                RelativeMovement.StopMovement(true);
                                Messenger.Broadcast("MathMission2");
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
                            Messenger.Broadcast("MathMission3");
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
                                RelativeMovement.StopMovement(true);
                                Messenger.Broadcast("MathMission4");
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
                                Messenger.Broadcast("MathMission5");
                            }
                            else if (tagInteraction.Contains(GameEvent.ARROW_GENERIC) && inventary[3] >= 1 && contPaper < 4)
                            {
                                string[] splitted = tagInteraction.Split(GameEvent.ARROW_GENERIC);
                                int pos = int.Parse(splitted[1]);
                                if(canInstantiate)
                                {
                                    TapeMovement.SetTapeMove(true);
                                    TapeMovement.SetStartPosition(cornerPosition[pos]);
                                    startPosition = cornerPosition[pos];
                                    startPositionInt = pos;
                                    tape1 = Instantiate(prefabsMission[5], startPosition, Quaternion.identity);
                                    canInstantiate = false;
                                }
                                
                                if((startPositionInt == 0 && pos == 1) && (!arch[0]))
                                {
                                    CreateTape(0,startPosition,141f,0f,180f,1.85f,0,4,8,"2 m.","-----------");
                                }
                                else if((startPositionInt == 1 && pos == 0) && (!arch[0]))
                                {
                                    CreateTape(0,startPosition,141f,0f,0f,1.85f,0,4,8,"2 m.","-----------");
                                }
                                else if((startPositionInt == 1 && pos == 2) && (!arch[1]))
                                {
                                    CreateTape(2,startPosition,219f,-90f,0f,2.9f,1,5,9,"3 m.","-------------");
                                }
                                else if((startPositionInt == 2 && pos == 1) && (!arch[1]))
                                {
                                    CreateTape(2,startPosition,219f,-90f,180f,2.9f,1,5,9,"3 m.","-------------");
                                }
                                else if((startPositionInt == 2 && pos == 3) && (!arch[2]))
                                {
                                    CreateTape(0,startPosition,219f,0f,0f,1.85f,2,6,10,"2 m.","-----------");
                                }
                                else if((startPositionInt == 3 && pos == 2) && (!arch[2]))
                                {
                                    CreateTape(0,startPosition,219f,0f,180f,1.85f,2,6,10,"2 m.","-----------");
                                }
                                else if((startPositionInt == 3 && pos == 0) && (!arch[3]))
                                {
                                    CreateTape(2,startPosition,141f,-90f,180f,2.9f,3,7,11,"3 m.","-------------");
                                }
                                else if((startPositionInt == 0 && pos == 3) && (!arch[3]))
                                {
                                    CreateTape(2,startPosition,141f,-90f,0f,2.9f,3,7,11,"3 m.","-------------");
                                }
                            }
                            if (contPaper >= 4 && inventary[3] >= 1)
                            {
                                Messenger.Broadcast("MathMission6");
                                RelativeMovement.StopMovement(true);
                                SpawnMark(GameEvent.CALC_TAG);
                                inventary[3] = 0;
                            }
                            else if (tagInteraction == GameEvent.CALC_TAG && inventary[4] <= 0)
                            {
                                RaccoltoOggetto(4);
                                Messenger.Broadcast("MathMission7");
                                SpawnMark(GameEvent.CLIPBOARD_TAG);
                            }
                            else if (tagInteraction == GameEvent.CLIPBOARD_TAG && inventary[4] >= 1)
                            {
                                RemoveMark();                        
                                var mathMission = _camera.GetComponent<MathMission>();
                                mathMission.enabled = true;
                                RelativeMovement.StopMovement(true);
                                Messenger.Broadcast("UIMathMission");
                                GameObject tmp = Instantiate(prefabsMission[7], new Vector3(18.537f,1.613f,20.863f), Quaternion.Euler(-66.7f,-19.982f,0f));
                                tmp.transform.name = "MissionCalculator";
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
    // Some stuff...
    // Per impedire che si facciano delle azioni senza i collezionabili necessari.
    // Appare un messaggio UI che ci avvisa
    public void notNow(){
        Debug.Log("Adesso non puoi !!");
        //Messenger.Broadcast(GameEvent.FORGET);
    }
    
    public void RaccoltoOggetto(int i){
        inventary[i] += 1;
        if(objectToDestroy!= null){
            Transform childText = objectToDestroy.gameObject.GetComponent<Transform>();
            Destroy(objectToDestroy.gameObject);
        }
    }
    public void LasciaOggetto(int i){
        if(inventary[i]>0){
            inventary[i]=0;
        }
        //Messenger.Broadcast(GameEvent.LANCIA_OGGETTO);
    }

    public int getOggettoCount(int i){
        return inventary[i];
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
            Debug.Log("Spawn");
        }
    }
    
    public void MissionArtDone()
    {
        Debug.Log("Missione Arte Completa");
        missionActive[1] = false;
        missionComplete[1] = true;
        Messenger.Broadcast("MissionComplete");
        if (!InMission(1))
        {
            Debug.Log(missionActive[1]);
            Debug.Log("Cerca in giro per la mappa gli interagibili relativi all'arte");
            SetComplete(1);
            missionType = 0;
        }
    }

    public void MathMissionDone()
    {
        RelativeMovement.StopMovement(false);
        player.gameObject.SetActive(true);
        SetComplete(2);
        missionType = 0;
        Destroy(GameObject.Find("MissionCalculator"));
        Messenger.Broadcast("MathCamDone");
        missionActive[1] = false;
        missionComplete[1] = true;
        var mathMission = _camera.GetComponent<MathMission>();
        mathMission.enabled = false;
    }
    void Awake()
    {
        Messenger.AddListener("MissionTutorialDone", MissionTutorialDone);
        Messenger.AddListener("MissionArtDone", MissionArtDone);
        Messenger.AddListener("MathMissionDone", MathMissionDone);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener("MissionTutorialDone", MissionTutorialDone);
        Messenger.RemoveListener("MissionArtDone", MissionArtDone);
        Messenger.RemoveListener("MathMissionDone", MathMissionDone);
    }

}