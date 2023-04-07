using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Lo script del tutorial
// Comportamento base: il fantasmino si muove "iterando" una serie di punti della mappa 
// "Guidando" il giocatore ai primi collezionabili
// E quando il giocatore si allontana si ferma e chiede di essere seguito

public class GhostMovement : MonoBehaviour
{
    // Il percorso che segue il fantasma
    private Vector3[] targetsFirstPastelli = new Vector3[4];
    private Vector3[] targetsSecondPastelli = new Vector3[4];
    private Vector3[] targetsFrigo = new Vector3 [3];

    private int reachedFirstPastelli = 0;
    private int reachedSecondPastelli = 0;
    private int reachedFrigo = 0;
    
    // Il player 
    [SerializeField] private Transform player;
    
    // Variabili utilizzate per fare fluttuare il fantasma
    public float speed = 0.2f;
    public float obstacleRange = 5.0f;
    private bool _alive;
    // Il player
    [SerializeField] private GameObject bimbo;
    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 0.5f;
    
    // Il fantasma 
    [SerializeField] public GameObject ghostTextWindow;
    // I vari testi che vengono disattivati e attivati a seconda delle esigenze 
    [SerializeField] private TMP_Text ghostTextMessage;
    [SerializeField] private TMP_Text ghostTextE; 
    [SerializeField] private TMP_Text labelMoves;
    [SerializeField] private TMP_Text labelTutorial;
    [SerializeField] public GameObject spaceBarLabel;
    [SerializeField] public GameObject imageText;


    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    private bool startMarachella = false;
    private bool endFirstMarachella = false;

    void Start(){
        // La strada che deve seguire il fantasmino
        targetsFirstPastelli[0] = new Vector3(25.6f,1.7f,15.8f);
        targetsFirstPastelli[1] = new Vector3(23.63f,1.7f,14.76f);
        targetsFirstPastelli[2] = new Vector3(20.0f,1.7f,14.33f);
        targetsFirstPastelli[3] = new Vector3(23.05f,1.7f,10.34f);
        // La strada che deve seguire il fantasmino per il tutorial
        targetsSecondPastelli[0] = new Vector3(22.6f, 1.7f, 14.0f);
        targetsSecondPastelli[1] = new Vector3(24.95f, 1.7f, 15.47f);
        targetsSecondPastelli[2] = new Vector3(25.67f, 1.7f, 10.7f);          
        targetsSecondPastelli[3] = new Vector3(29.0f, 1.7f, 11.4f);

        targetsFrigo[0] = new Vector3(26.05f,1.7f,14.33f);
        targetsFrigo[1] = new Vector3(26.73f,1.7f,18.2f);
        targetsFrigo[2] = new Vector3(29.3f,1.7f,20.0f);

        _alive=true;
        posOffset = transform.position;
        ghostTextE.text = UIMessages.EMPTY_MESSAGE;
    }

    void Update()
    {
        if(!endFirstMarachella)
            move();
        Float(); 
    }

    // Serve per far fluttuare il fantasma
    private void Float(){
       
        tempPos = posOffset;
        tempPos.x = transform.position.x;
        tempPos.z = transform.position.z;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    // Inizio prima marachella
    // e inizio tutorial
    public void PrimaMarachella(){
        ghostTextMessage.text = UIMessages.FIND_ME_LABEL;
        ghostTextE.text = UIMessages.EMPTY_MESSAGE;
        startMarachella = true;
    }
    
    private void move(){

        if (reachedFirstPastelli < targetsFirstPastelli.Length){
            // Guarda la posizione
            transform.LookAt(targetsFirstPastelli[reachedFirstPastelli]);
            // La distanza del target
            float distanceWithTarget = Vector3.Distance(targetsFirstPastelli[reachedFirstPastelli], this.gameObject.transform.position);
            if (distanceWithTarget < 0.05f) {
                reachedFirstPastelli++;
            }
            else {
                // La posizione del bambino
                float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
                // Se la distanza tra il bambino e il fantasmino è minore di 1, il fantasmino va verso il raget      
                if(_alive && distance < 1)
                {
                    transform.Translate(0, 0, speed*Time.deltaTime);

                    Ray ray = new Ray(transform.position, transform.forward);
                    RaycastHit hit;
                    ghostTextWindow.SetActive(false);

                }
                else {
                    // Altrimenti il fantasmino si ferma.
                    // Viene attivato il testo sulla testa di quest'ultimo che in fine guarderà il player per via di "LookAt(player)"
                    ghostTextWindow.SetActive(true);
                    transform.LookAt(player);
                }
            }            
        } else if (startMarachella) {
            // Se la prima marachella è iniziata
            // il fantasmino segue le posizioni di targetsSecondPastelli e in fine di targetFrigo
            // per finire il tutorial
            labelTutorial.text = UIMessages.RACCOGLI_PASTELLI;
            if(reachedSecondPastelli < targetsSecondPastelli.Length){
                transform.LookAt(targetsSecondPastelli[reachedSecondPastelli]);
                float distanceWithTarget = Vector3.Distance(targetsSecondPastelli[reachedSecondPastelli], this.gameObject.transform.position);
                if (distanceWithTarget < 0.05f) {
                    reachedSecondPastelli++;
                }
                else {
                    float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
                    if(_alive && distance < 1.7)
                    {
                        transform.Translate(0, 0, speed*Time.deltaTime);
                        ghostTextWindow.SetActive(false);
                    }
                    else {
                        ghostTextWindow.SetActive(true);
                        transform.LookAt(player);
                    }
                }
            } else if (reachedSecondPastelli == targetsSecondPastelli.Length && reachedFrigo < targetsFrigo.Length){
                labelTutorial.text = UIMessages.FRIGO_MESSAGE;
                transform.LookAt(targetsFrigo[reachedFrigo]);
                float distanceWithTarget = Vector3.Distance(targetsFrigo[reachedFrigo], this.gameObject.transform.position);
                if (distanceWithTarget < 0.05f) {
                    reachedFrigo++;
                }
                else {
                    float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
                
                    if(_alive && distance < 1.5)
                    {
                        transform.Translate(0, 0, speed*Time.deltaTime);
                        ghostTextWindow.SetActive(false);
                    }
                    else {
                        ghostTextWindow.SetActive(true);
                        transform.LookAt(player);
                    }
                }
            } else if(reachedFrigo == targetsFrigo.Length){
                transform.LookAt(player);
                labelTutorial.text = UIMessages.EMPTY_MESSAGE;
                endFirstMarachella=true;
            }
        } else { 
            // Aziona la finestra del fantasmino per l'inizio del tutorial
            float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
            transform.LookAt(player);
            ghostTextWindow.SetActive(true);
            ghostTextMessage.text = UIMessages.START_TUTORIAL_LABEL;
            // Se la distanza è minore di 1 scrive "E" per fare capire che è possibile interagire per l'inizio del tutorial
            // altrimenti rimuove la "E"
            if (distance < 1){
                ghostTextE.text = UIMessages.E_MESSAGE;
                if (labelTutorial.text.Contains(UIMessages.START_MESSAGE)){
                    labelTutorial.text = UIMessages.EMPTY_MESSAGE;
                }
                labelMoves.text = UIMessages.EMPTY_MESSAGE;
                spaceBarLabel.SetActive(false);                 
            }else{
                ghostTextE.text = UIMessages.EMPTY_MESSAGE; 
            }
        }         
    }

    void Awake(){
        Messenger.AddListener(GameEvent.PRIMA_MARACHELLA, PrimaMarachella);
    }

    public void SetAlive(bool alive){
        _alive = alive;
    }

     void OnDestroy() {
        Messenger.RemoveListener(GameEvent.PRIMA_MARACHELLA, PrimaMarachella); 
    }
}
