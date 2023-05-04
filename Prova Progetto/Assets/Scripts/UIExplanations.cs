using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
public class UIExplanations : MonoBehaviour
{

    [SerializeField] private Canvas messagesCanvas;
    [SerializeField] private TMP_Text textMission;
    [SerializeField] private CinemachineVirtualCamera[] tutorialCameras;
    [SerializeField] private CinemachineFreeLook mainCamera;
    
    private IEnumerator FirstWords()
    {
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "BENVENUTO NEL TUTORIAL...";
        yield return new WaitForSeconds(5);
        textMission.text = "INIZIAMO A VEDERE COME MUOVERSI.";
        yield return new WaitForSeconds(5);
        BambinoControllerAngiolettoMode.talking = false;
    }

    private IEnumerator MarkWords()
    {
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "GUARDA, ALLA TUA DESTRA E' APPARSO QUALCOSA...";
        yield return new WaitForSeconds(5);
        mainCamera.gameObject.SetActive(false);
        tutorialCameras[0].gameObject.SetActive(true);
        textMission.text = "QUESTO SIMBOLO INDICA CHE C'E' UNA NUOVA MISSIONE DA POTER INIZIARE...";
        yield return new WaitForSeconds(5);
        textMission.text = "OGNI VOLTA CHE VUOI INTERAGIRE CON UNO DI QUESTI SIMBOLI AVVICINATI E LO VEDRAI ILLUMINARSI,";
        yield return new WaitForSeconds(5);
        textMission.text = "E QUINDI POTRAI INTERAGIRE CON LUI.";
        yield return new WaitForSeconds(5);
        textMission.text = "ORA PROVA A AVVICINARTI E INTERAGIRE CON LUI PREMENDO IL TASTO -E- !!";
        yield return new WaitForSeconds(5);
        textMission.text = "USA I TASTI (W,A,S,D, [SPACE]) PER MUOVERTI E SALTARE E ARRIVA DI FRONTE IL PUNTO ESCLAMATIVO -!-";
        yield return new WaitForSeconds(5);
        BambinoControllerAngiolettoMode.talking = false;
        RelativeMovement.SetInMission(false);
    }

    private IEnumerator ReturnToMainCamera()
    {
        yield return new WaitForSeconds(10);
        mainCamera.gameObject.SetActive(true);
        tutorialCameras[0].gameObject.SetActive(false);

    }
    // Creare un cam Manager
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void FirstExplanation()
    {
        StartCoroutine(FirstWords());
    }
    private void SecondExplanation()
    {
        StartCoroutine(MarkWords());
        StartCoroutine(ReturnToMainCamera());
    }
    void Awake()
    {
        Messenger.AddListener("FirstExplanation", FirstExplanation);
        Messenger.AddListener("SecondExplanation", SecondExplanation);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener("FirstExplanation", FirstExplanation);
        Messenger.RemoveListener("SecondExplanation", SecondExplanation);
    }
}
