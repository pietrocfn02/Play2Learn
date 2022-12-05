using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestisce i suoni all'interno delle scene
public class AudioManager : MonoBehaviour
{
    
    // La clip che deve essere riprodotta
    private AudioClip audioTv;
    // Il livello dell'audio
    private static float audioLevel;
    // L'oggetto da cui deriva il suono 
    private string source;
    // Serve per assegnare la clip al GameObject
    private GameObject[] type;
    //L' audio generale delle varie scene 
    private AudioSource audioAmbiance;

    
    void Start(){
        //Prendo il componente AudioSource su cui è stato "attaccato" lo script Audio Manager
        audioAmbiance = GetComponent<AudioSource>();
        //Parte la clip ambiente, assegnata nell'ispector, della scena corrente 
        audioAmbiance.Play();
    }

    void Update(){
        //Prendo il valore corrente del volume della clip assegnata a AudioSource
        audioAmbiance.volume = audioLevel;
    }


    //Tutti i metodi utili per riprodurre i suoni presentano la medesima logica

    // Cambia il materiale delle televisioni per simulare lo spegnimento della TV
   public void turnOffTV(string tvSource, AudioClip clip){
        //Il tag della TV
        source = tvSource;
        //Cerco la TV nella scena
        type =  GameObject.FindGameObjectsWithTag(source);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = clip;
        //Riproduco la clip
        audioSource.PlayOneShot(audioTv);
    }
    
    //Metodo che riproduce la clip della seconda missione
    public void reproducePem(AudioClip pem){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.TV_CUCINA_TAG);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = pem;
        audioSource.PlayOneShot(audioTv);
    }
    //Metodo che ferma la clip della TV 
    public void stopPem(AudioClip pem){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.TV_CUCINA_TAG);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioSource.Stop();
    }
    //Metodo che riproduce la clip della collezione
    public void collect(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }
    //Metodo che riproduce il rilascio dei pastelli all'interno del contenitore
    public void releaseObject(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.volume = audioLevel;
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }
    /*
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
    */

    //Prende la variabile dallo slider in GameSettings
    public static void setAudio(float level){
        audioLevel = level;
    }
}
