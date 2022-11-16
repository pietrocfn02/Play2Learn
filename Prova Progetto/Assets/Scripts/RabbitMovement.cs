using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private Transform camera;
    
    private Vector3[] bunnyTargets = new Vector3[10];

    private bool muoviti = false;
    // 22.8 X 13.8 Z
    // X        Y 
    // 26.5     15.5
    void Start(){}

    void Update(){
        transform.LookAt(camera);
        if (muoviti){
            transform.Translate(0.7f,0f,0.7f);
            muoviti = false;
        }
    }
    void Awake() {
        Messenger.AddListener(GameEvent.START_TUTORIAL, startTutorial);   
    }

    public void startTutorial(){
        muoviti = true;
    }
}

