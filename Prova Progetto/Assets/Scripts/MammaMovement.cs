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
        if (updates % 1000 == 0) {
            speed= speed + 0.2f;
        }
        target = player.position;
        target.y = 2.0f;
        move();
        Float(); 
        updates++;
        Debug.Log(updates);
    }


    
    private void move(){

        transform.LookAt(target);
        float distancewithTarget = Vector3.Distance(target, this.gameObject.transform.position);
        Debug.Log(distancewithTarget);
        if (distancewithTarget < 0.33f) {
            Debug.Log("PRESA, STUPIDA BAMBINA!");
        }
        else {
            float distance = Vector3.Distance(player.transform.position,this.gameObject.transform.position);
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
