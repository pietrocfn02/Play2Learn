using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


using UnityEngine.SceneManagement;




// Script di comportamento dell'inseguitore nella scena dell'inseguimento
public class MammaMovement : MonoBehaviour
{
    // Il percorso che segue il fantasma
    private Vector3 target;

    private int reachedTarget = 0;
    
    // Il player 
    [SerializeField] private Transform player;
    
    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 0.5f;

    // Variabili per che servono per fare fluttuare il fantasma
    public float speed = 0.2f;
    public float obstacleRange = 5.0f;

    // Ogni tot updates l'inseguitore respawna in un nuovo punto completamente casuale
    // Mi serve sapere quante volte respawna perche dopo tot respawns deve diventare piu forte
    private int respawns = 0;


    // Conto quante volte chiamo il metodo Update
    // Per evitare coroutine gestisco gli eventi dipendenti dal tempo tramite 
    // Il numero di updates ( Tanto non mi serve essere troppo preciso, sono tutti eventi che hanno bisogno di tempo approssimativo)
    private int updates = 1;


    // Mi salvo l'update nel quale il fantasma si e' avvicinato a me
    // Cosi che io possa controlalre dopo un po di update se sono riuscito a scappare e quindi continuo l'inseguimento
    // Oppure sono stato preso e quindi finisco la scena
    private int updateTaken = -1;

    // Per il floating 
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    
    void Start(){
      
        posOffset = transform.position;
    }
    void Update()
    {
        
        // Ogni 100 updates il fantasma accelera di + 10%
        if (updates % 100 == 0) {
            speed= speed + (speed*0.1f);
        }
        
        target = player.position;
        target.y = 2.0f;
        if (updates % 2500 == 2499) {
            // Ogni 2500 update il fantasma respawna in un punto a caso della mappa
            transform.position = new Vector3(Random.Range(8,16), 2.0f, Random.Range(4,24));
            this.respawns ++;
        }
        else {

            // Mi muovo 
            move();

            //Fluttuo
            Float(); 
        }
        
        // Conto gli updates
        updates++;
        
    }


    
    private void move(){

        transform.LookAt(target);
        float distancewithTarget = Vector3.Distance(target, this.gameObject.transform.position);

        if (updateTaken != -1) {

            // 45 updates e' il limite che ho per scappare
            if (updates - updateTaken > 45) {
                
                // Se al 46esimo update sono ancora vicino ho perso
                // Altrimenti setto updateTaken a -1 che e' l'equivalente di dire "non sono mai stato preso"
                if (distancewithTarget < 0.5f) {
                    SceneManager.LoadScene("Storytelling_angioletto");

                }
                else {
                    updateTaken = -1;
                }
            }
        }

        // Questo indica che sono stato preso
        if (distancewithTarget < 0.35f) {
            if (updateTaken == -1) {
                updateTaken = updates;
            }
        }
        
        else {
            // Il power up del fantasma dal terzo respawn in poi
            // Quando ci si avvicina sotto 1.9 di distanza aumenta di 6 volte la sua velocita' 
            // Peccato che ci becca quasi sempre prima,
            // perche' quest'effetto e' molto bello graficamente
            if (distancewithTarget < 1.9f && this.respawns > 2) {
                transform.Translate(0, 0, speed*Time.deltaTime*6);
            }
            transform.Translate(0, 0, speed*Time.deltaTime);
        }     
        
    }

    // Fluttuo
    private void Float(){
       
        tempPos = posOffset;
        tempPos.x = transform.position.x;
        tempPos.z = transform.position.z;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    
    void OnDestroy() {
    }
}
