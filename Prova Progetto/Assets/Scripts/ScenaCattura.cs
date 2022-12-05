using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ScenaCattura : MonoBehaviour
{

    // Storytelling che annuncia la angioletto mode
    // Il fantasmino si avvicina al personaggio "con fare minaccioso"

    // Simile a "Fantasmini arrabbiati" ma leggermente diverso
 
    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 1f;

    private Vector3 target = new Vector3(25.7f, 2f, 9.1f);

    [SerializeField] StoryTelling storyTelling;

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    
    private bool onGoal = false;

    void Start(){
        posOffset = transform.position;
    }
    void Update()
    {
        Float();
        Move(); 
    }
    
    private void Float(){
        
        tempPos = posOffset;
        tempPos.z = transform.position.z;
        tempPos.x = transform.position.x;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
    

    private void Move() {
        if (!onGoal){
            Vector3 tmpTarget = target;
            tmpTarget.y = 2.06f;
            transform.LookAt(tmpTarget);
            
            float distanceWithTarget = Vector3.Distance(target, transform.position);
            if (distanceWithTarget > 0.55f) {
                if (distanceWithTarget < 0.75f) {
                    transform.Translate(0, 0, 1f*Time.deltaTime);
                }
                else {
                    transform.Translate(0, 0, 0.3f*Time.deltaTime);
                }
            }
            else {
                onGoal = true;
                storyTelling.canStartTelling = true;
            }
        }
    }
    void OnDestroy() {
    }
}
