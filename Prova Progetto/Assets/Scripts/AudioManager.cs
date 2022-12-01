using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestisce i suoni all'interno delle scene
public class AudioManager : MonoBehaviour
{
    private AudioSource audio;
    
    
    void Start(){
        audio = GetComponent<AudioSource>();
        Debug.Log(audio.name);
    }
    public void  ChangeAudio(){
        
        
    }
    void Update()
    {
        if (audio != null){
            Debug.Log("Non è null");
            audio.Play();
        }
    }
}
