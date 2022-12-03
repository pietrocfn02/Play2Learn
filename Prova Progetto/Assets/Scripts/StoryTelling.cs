using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;

using TMPro;

public static class Constants {

    public static string SCENE_INTRO = "INTRO";
    public static string EMPTY = "";
    public static string SCENE_CHASING = "CHASE";
    public static string NEXT = "PREMI INVIO PER ANDARE AVANTI";
    public static string NEW_GAME = "QUANDO SEI PRONTA PREMI NUOVAMENTE INVIO";
}

public class StoryTelling : MonoBehaviour
{
    // Start is called before the first frame update
    
    private static string[] completeIntro = {
        "4 DICEMBRE 2022. E' DOMENICA, QUINDI, PER LA GIOIA DI NOI BAMBINI, NIENTE SCUOLA",
        "IL GIORNO PERFETTO PER FARE MARACHELLE INSIEME AL PROPRIO AMICO IMMAGINARIO", 
        "ESPLORA LA CASA, SCOPRI I SUOI SEGRETI E DIVERTITI A COMBINARE CASINI",
        "MA ATTENTA A NON ESAGERARE, POTRESTI FINIRE NEI GUAI",
        "BUON DIVERTIMENTO!"};
    
    private static  string[] completeChase = {
        "OH NO... QUEL FANTASMINO SEMBRA MOLTO ARRABBIATO",
            "SI DIRIGE VERSO LA LIBRERIA",
            "NON STARA' MICA PENSANDO DI FARCI FARE I COMPITI?",
            "FORSE CI CONVIENE SCAPPARE PRIMA CHE SIA TROPPO TARDI",
            "....."
    };

    private string[] completeText = completeIntro;

    private float waitTime = 0.15f;
    private float currentWaitTime = 0.15f;

    [SerializeField] TMP_Text customizableText;

    [SerializeField] TMP_Text next;

    [SerializeField] string scene;

    [SerializeField] GameObject compitiPrefab;
    private Vector3 nextCompitiSpawn = new Vector3(26f,1.98f,9.1f);

    private bool nextB;
    
    private int fraseAttuale = 0;
    private int updates = 0;

    private int spawns = 0;


    public bool canStartTelling = false;

    void Start()
    { 
        customizableText.text = Constants.EMPTY;
        nextB = false;
        fraseAttuale = 0;
        next.text = Constants.EMPTY;

        
    }


    void Update(){
    
        if (scene == Constants.SCENE_CHASING) {
           completeText = completeChase;
        }
        else {
            canStartTelling = true;
        }

        if (canStartTelling){
            if (scene==Constants.SCENE_CHASING && compitiPrefab != null && updates % 70 == 0 && spawns < 11)  {
                GameObject x = Instantiate(compitiPrefab) as GameObject;
                Vector3 spawnPosition = nextCompitiSpawn;
                nextCompitiSpawn.y = nextCompitiSpawn.y + 0.08f;
                x.transform.position = spawnPosition;
                spawns++;
            }
            currentWaitTime = currentWaitTime - Time.deltaTime;
            if (currentWaitTime <= 0) {
                Scrivi();
                currentWaitTime = waitTime;
            }
            if (nextB) {
                Debug.Log("Sto aspettando input...");
                Debug.Log("QUI :) 2");
                if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.E)){
                    Debug.Log("PREMO Invio");
                    next.text = Constants.EMPTY;
                    if (fraseAttuale >= completeText.Length-1) {
                        if (scene == Constants.SCENE_CHASING) {
                            Debug.Log("QUI :)");
                            SceneManager.LoadScene("InseguimentoScene");
                        }
                        else {
                            SceneManager.LoadScene("Diavoletto_Scene");
                        }
                        
                        
                    }
                    else {
                        customizableText.text = Constants.EMPTY;
                        fraseAttuale++;
                    }
                    nextB = false;   
                }
                
            }
            else {
                if (fraseAttuale < completeText.Length) {
                    if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.E)){
                        customizableText.text = completeText[fraseAttuale];
                        currentWaitTime = waitTime;
                        nextB = true;                
                    }
                }
            }
        }
        
        updates++;
    }

    void Scrivi() {
        if (fraseAttuale < completeText.Length && customizableText.text.Length < completeText[fraseAttuale].Length) {
            customizableText.text = customizableText.text + completeText[fraseAttuale][customizableText.text.Length];
        }
        else {
            nextB = true;
            if (fraseAttuale >= completeText.Length-1) {
                next.text = Constants.NEW_GAME;
            }
            else {
                next.text = Constants.NEXT;
            }
            
        }
    }


}
