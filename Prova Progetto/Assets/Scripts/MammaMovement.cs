using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



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
    private bool _alive;   


    private int respawns = 0;
    private int updates = 1;

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    private bool startMarachella = false;
    void Start(){
      
        _alive=true;
        posOffset = transform.position;
    }
    void Update()
    {
        if (updates % 100 == 0) {
            speed= speed + (speed*0.1f);
        }
        
        target = player.position;
        target.y = 2.0f;
        if (updates % 2500 == 2499) {
            transform.position = new Vector3(Random.Range(8,16), 2.0f, Random.Range(4,24));
            this.respawns ++;
        }
        else {
            move();
            Float(); 
        }
        
        updates++;
        Debug.Log(updates);
    }


    
    private void move(){

        transform.LookAt(target);
        float distancewithTarget = Vector3.Distance(target, this.gameObject.transform.position);
        Debug.Log(distancewithTarget);
        if (distancewithTarget < 0.35f) {
            Debug.Log("PRESA, STUPIDA BAMBINA!");
        }
        
        else {
            if (distancewithTarget < 1.9f /*&& this.respawns > 2*/) {
                Debug.Log("TURBOOOOO");
                transform.Translate(0, 0, speed*Time.deltaTime*6);
            }
            transform.Translate(0, 0, speed*Time.deltaTime);
        }     
        
    }

    private void Float(){
       
        tempPos = posOffset;
        tempPos.x = transform.position.x;
        tempPos.z = transform.position.z;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    public void SetAlive(bool alive){
        _alive = alive;
    }
    
    void OnDestroy() {
    }
}
