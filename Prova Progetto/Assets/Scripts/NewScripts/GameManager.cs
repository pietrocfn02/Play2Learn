using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool W_TAP;
    private bool A_TAP;
    private bool S_TAP;
    private bool D_TAP;

    IEnumerator TutorialMovement()
    {
        //Messenger.Broadcast("MISSION_ON");
        Debug.Log("Benvenuto nel tutorial...");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Iniziamo a vedere come muoversi.");
        yield return new WaitForSeconds(2);
        Debug.Log("Per prima cosa usa i comandi ( W,A,S,D, [SPACE] ) per muoverti e saltare.");
        yield return new WaitForSeconds(10);
        Time.timeScale = 0;
        yield return new WaitForSeconds(5);
        Debug.Log("Vai verso l'oggetto illuminato e collezionalo.");
        Time.timeScale = 1;
    }
    //Vado verso l'oggetto illuminato, entra nel trigger e avvio la spiegazione dei collezionabili
    IEnumerator TutorialCollectibles()
    {

    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    // Se premo sul pulsante della scena iniziale tramite broadcast mi permette di 
    // ricevere un segnale che indica l'inizio del gioco
    private void StartGame()
    {
        
    }
    // Permette di iniziare il tutorial
    private void StartTutorial()
    {
        StartCoroutine(TutorialMovement());
    }

    void Awake()
    {
        //Messenger.AddListener(GameEvent.START_GAME, StartGame);
        Messenger.AddListener(GameEvent.START_TUTORIAL, StartTutorial);
    }

    void OnDestroy()
    {
        //Messenger.RemoveListener(GameEvent.START_GAME, StartGame);
        Messenger.RemoveListener(GameEvent.START_TUTORIAL, StartTutorial);
    }
}
