using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestisce i suoni all'interno delle scene
public class AudioManager : MonoBehaviour
{
    
    private AudioClip audioTv;
    
    private string source;
    GameObject[] type;
    public void turnOffTV(string tvSource, AudioClip clip){
        source = tvSource;
        type =  GameObject.FindGameObjectsWithTag(source);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }
    
    public void reproducePem(AudioClip pem){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.TV_CUCINA_TAG);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioTv = pem;
        audioSource.PlayOneShot(audioTv);
    }
    public void stopPem(AudioClip pem){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.TV_CUCINA_TAG);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.Stop();
    }
    public void collect(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }
    public void releaseObject(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }
    public void stopCollect(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioSource.Stop();
    }
    public void walk(AudioClip clip){
        type =  GameObject.FindGameObjectsWithTag(GameEvent.PLAYER);
        AudioSource audioSource = type[0].GetComponent<AudioSource>();
        audioTv = clip;
        audioSource.PlayOneShot(audioTv);
    }

}
