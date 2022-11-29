using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class GhostMovement : MonoBehaviour
{
    // Il percorso che segue il fantasma
    private Vector3[] targets = new Vector3[6];

    private int reachedTarget = 0;
    
    // Il player 
    [SerializeField] private Transform player;
    
    // Variabili utilizzate per fare fluttuare il fantasma
    public float speed = 0.2f;
    public float obstacleRange = 5.0f;
    private bool _alive;
    [SerializeField] private GameObject bimbo;
    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 0.5f;
    
    // Il fantasma e i vari testi che vengono disattivati e attivati a seconda delle esigenze 
    [SerializeField] public GameObject peppino;
    [SerializeField] private TMP_Text peppinoText;
    [SerializeField] private TMP_Text peppinoInteract; 
    [SerializeField] private TMP_Text labelMoves;
    [SerializeField] private TMP_Text labelTutorial;


    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    private bool startMarachella = false;

    void Start(){
        targets[0] = new Vector3(25.6f,1.7f,15.8f);
        targets[1] = new Vector3(23.63f,1.7f,14.76f);
        targets[2] = new Vector3(20f,1.7f,14.33f);
        targets[3] = new Vector3(23.05f,1.7f,10.34f);
        targets[4] = new Vector3(28.85f, 1.7f, 11.5f);
        targets[5] = new Vector3(29.3f,1.7f,20.5f);

        _alive=true;
        posOffset = transform.position;
        peppinoInteract.text = "";
    }
    void Update()
    {
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
        peppinoText.text = "Trovami!!";
        startMarachella = true;
    }

    
    private void move(){

        if (reachedTarget < 4){
            transform.LookAt(targets[reachedTarget]);
            float distancewithTarget = Vector3.Distance(targets[reachedTarget], this.gameObject.transform.position);
            if (distancewithTarget < 0.05f) {
                reachedTarget++;
            }
            else {
                float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
            
                if(_alive && distance < 1)
                {
                    transform.Translate(0, 0, speed*Time.deltaTime);

                    Ray ray = new Ray(transform.position, transform.forward);
                    //RaycastHit hit;
                    peppino.SetActive(false);
                }
                else {
                    peppino.SetActive(true);
                    transform.LookAt(player);
                }
            }            
        }
        else if(startMarachella) {
            peppinoInteract.text = "";
            transform.LookAt(targets[4]);
            float distancewithTarget = Vector3.Distance(targets[4], this.gameObject.transform.position);
            if (distancewithTarget > 0.05f) {
                float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
                if(_alive && distance < 2)
                {
                    transform.Translate(0, 0, speed*Time.deltaTime);

                    Ray ray = new Ray(transform.position, transform.forward);
                    //RaycastHit hit;
                    peppino.SetActive(false);
                }
                else {
                    peppino.SetActive(true);
                    transform.LookAt(player);
                }
            } else {
                transform.LookAt(targets[5]);
                distancewithTarget = Vector3.Distance(targets[5], this.gameObject.transform.position);
                if (distancewithTarget > 0.05f) {
                    float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
                
                    transform.Translate(0, 0, speed*Time.deltaTime);

                    Ray ray = new Ray(transform.position, transform.forward);
                    //RaycastHit hit;
                    peppino.SetActive(false);
                }
            }     
        } else { 
            float distance = Vector3.Distance(bimbo.transform.position,this.gameObject.transform.position);
            transform.LookAt(player);
            peppino.SetActive(true);
            peppinoText.text = "Start tutorial";
            if (distance < 1){
                peppinoInteract.text = "E";
                if (labelTutorial.text.Contains("Usa i tasti per muoverti")){
                    labelTutorial.text="";
                }
                labelMoves.text="";
                 
            }else{
                peppinoInteract.text = ""; 
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
