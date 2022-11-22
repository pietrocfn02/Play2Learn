using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GhostMovement : MonoBehaviour
{

    private Vector3[] targets = new Vector3[4];

    private int reachedTarget = 0;
    
    [SerializeField] private Transform player;
    public float speed = 0.2f;
    public float obstacleRange = 5.0f;
    private bool _alive;
    [SerializeField] private GameObject bimbo;
    [SerializeField] private float degreesPerSecond = 15.0f;
    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 0.5f;
    [SerializeField] public GameObject peppino;
    [SerializeField] private TMP_Text peppinoText;
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    void Start(){
        targets[0] = new Vector3(25.6f,1.7f,15.8f);
        targets[1] = new Vector3(23.63f,1.7f,14.76f);
        targets[2] = new Vector3(20f,1.7f,14.33f);
        targets[3] = new Vector3(23.05f,1.7f,10.34f);
         _alive=true;
         posOffset = transform.position;
    }
    void Update()
    {
       
        move();
        Float(); 
    }

    private void Float(){
       
        tempPos = posOffset;
        tempPos.x = transform.position.x;
        tempPos.z = transform.position.z;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    } 
    private void move(){

        if (reachedTarget < targets.Length){
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
                    RaycastHit hit;
                    peppino.SetActive(false);
                }
                else {
                    peppino.SetActive(true);
                    
                    transform.LookAt(player);
                }
            }            
        }
        else { 
            peppino.SetActive(true);
            peppinoText.text = "pepino";
            transform.LookAt(player);
        }

         
    }
    public void SetAlive(bool alive){
        _alive = alive;
    }

}
