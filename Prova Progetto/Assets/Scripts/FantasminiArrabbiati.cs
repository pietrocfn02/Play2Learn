using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class FantasminiArrabbiati : MonoBehaviour
{
 
    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 1f;

    private Vector3 target = new Vector3(26.53f, 2.28f, 7.21f);

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
            transform.LookAt(target);
            float distanceWithTarget = Vector3.Distance(target, transform.position);
            Debug.Log("DWT: "+distanceWithTarget);
            if (distanceWithTarget > 0.05f) {
                if (distanceWithTarget < 0.45f) {
                    transform.Translate(0, 0, 2f*Time.deltaTime);
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
