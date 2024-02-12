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


}
