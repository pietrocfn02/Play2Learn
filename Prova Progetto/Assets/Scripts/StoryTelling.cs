using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using TMPro;

public static class Constants {
    public static string FIRST = "DOMENICA 4 DICEMBRE 2022, UNA BAMBINA SI SVEGLIA E SA CHE DOVRA' CORRERE";
    public static string SECOND = "NON IMPORTA CHE TU SIA UN CROTALO O UN PAVONE...";
    public static string THIRD = "L'IMPORTANTE E' CHE SE MUORI ME LO DICI PRIMA";
    public static string EMPTY = "";

    public static string NEXT = "PREMI INVIO PER ANDARE AVANTI";
    public static string NEW_GAME = "PREMI INVIO PER INIZIARE UNA NUOVA PARTITA";
}

public class StoryTelling : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] completeText = {Constants.FIRST, Constants.SECOND, Constants.THIRD};

    private float waitTime = 0.15f;
    private float currentWaitTime = 0.15f;

    [SerializeField] TMP_Text customizableText;

    [SerializeField] TMP_Text next;


    private bool nextB;
    
    private int fraseAttuale = 0;

    void Start()
    { 
        customizableText.text = Constants.EMPTY;
        nextB = false;
        fraseAttuale = 0;
        next.text = Constants.EMPTY;
    }


    void Update(){
        currentWaitTime = currentWaitTime - Time.deltaTime;
        if (currentWaitTime <= 0) {
            Scrivi();
            currentWaitTime = waitTime;
        }
        if (nextB) {
            Debug.Log("Sto aspettando input...");
             if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.E)){
                Debug.Log("PREMO Invio");
                next.text = Constants.EMPTY;
                if (fraseAttuale >= completeText.Length-1) {
                    Debug.Log("Carico la scena Diavoletto");
                    SceneManager.LoadScene("Diavoletto_Scene");
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
