using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestisce i suoni all'interno delle scene
public class AudioManager : MonoBehaviour
{
    
    private AudioClip audioTv;
    private static float audioLevel;
    private string source;
    private GameObject[] type;
    
    AudioSource audioAmbiance;

    void Start(){
        audioAmbiance = GetComponent<AudioSource>();
        audioAmbiance.Play();
    }

    void Update(){
        audioAmbiance.volume = audioLevel;
    }


    // Funzioni utili per riprodurre tutti i suoni
   public void turnOffTV(string tvSource, AudioClip clip){
        source = tvSource;
        type =  GameObject.FindGameObjectsWithTag(source);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }
    
    public void reproducePem(AudioClip pem){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.TV_CUCINA_TAG);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = pem;
        audioSource.PlayOneShot(audioTv);
    }
    public void stopPem(AudioClip pem){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.TV_CUCINA_TAG);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioSource.Stop();
    }
    public void collect(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }
    public void releaseObject(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }
    public void stopCollect(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioSource.Stop();
    }
    public void walk(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }

    //Setta solo la variabile prendendola dallo slider in GameSettings
    public static void setAudio(float level){
        audioLevel = level;
    }


}
