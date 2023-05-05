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
    [SerializeField] private Canvas victoryCanvas;
    [SerializeField] private CinemachineVirtualCamera[] tutorialCameras;
    [SerializeField] private CinemachineFreeLook mainCamera;
    [SerializeField] private GameObject player;

    private Animator victory;

    private IEnumerator MarkWords()
    {

        messagesCanvas.gameObject.SetActive(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "GUARDA, ALLA TUA DESTRA E' APPARSO QUALCOSA...";
        yield return new WaitForSeconds(2);
        mainCamera.gameObject.SetActive(false);
        tutorialCameras[0].gameObject.SetActive(true);
        textMission.text = "QUESTO SIMBOLO INDICA CHE C'E' UNA NUOVA MISSIONE DA POTER INIZIARE...";
        yield return new WaitForSeconds(2);
        textMission.text = "OGNI VOLTA CHE VORRAI, AVVICINATI E VEDRAI UNO DI QUESTI SIMBOLI ILLUMINARSI,";
        yield return new WaitForSeconds(2);
        textMission.text = "CIO' SIGNIFICA CHE POTRAI INTERAGIRE CON LUI.";
        yield return new WaitForSeconds(2);
        textMission.text = "ORA PROVA A AVVICINARTI E PREMERE IL TASTO -E- !!";
        yield return new WaitForSeconds(2);
        textMission.text = "USA I TASTI (W,A,S,D, [SPACE]) PER MUOVERTI E SALTARE E ARRIVA DI FRONTE IL PUNTO ESCLAMATIVO -!-";
        yield return new WaitForSeconds(2);
        messagesCanvas.gameObject.SetActive(false);
        BambinoControllerAngiolettoMode.talking = false;
        RelativeMovement.SetInMission(false);
    }

    private IEnumerator PrefabMissionWords()
    {

        messagesCanvas.gameObject.SetActive(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "UNA VOLTA PREMUTO -E- COMPARIRANNO DEI SIMBOLI SOTTO L'OGGETTO INTERAGIBILE.";
        yield return new WaitForSeconds(2);
        mainCamera.gameObject.SetActive(false);
        tutorialCameras[0].gameObject.SetActive(true);
        textMission.text = "COME PUOI VEDERE OGNUNO DI QUESTI SIMBOLI SBLOCCA UNA MISSIONE E";
        yield return new WaitForSeconds(2);
        tutorialCameras[0].gameObject.SetActive(false);
        tutorialCameras[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        textMission.text = "UNA VOLTA FINITA LA MISSIONE TI VERRANNO SBLOCCATI ALTRI OGGETTI IN GIRO PER LA MAPPA CHE HANNO QUALCOSA DA RACCONTARE,";
        yield return new WaitForSeconds(2);
        textMission.text = "BASTERA' SOLTANTO AVVICINARTI E PREMERE -E-";
        yield return new WaitForSeconds(5);
        messagesCanvas.gameObject.SetActive(false);
        BambinoControllerAngiolettoMode.talking = false;
        RelativeMovement.SetInMission(false);
    }

    private IEnumerator EndTutorialWord()
    {
        messagesCanvas.gameObject.SetActive(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "BENE!!! HAI COMPLETATO CORRETTAMENTE IL TUTORIAL!";
        yield return new WaitForSeconds(2);
        textMission.text = "ADESSO NON TI RIMANE CHE GIROVAGARE PER CASA E IMPARARE.";
        yield return new WaitForSeconds(2);
        BambinoControllerAngiolettoMode.talking = false;
        messagesCanvas.gameObject.SetActive(false);
        RelativeMovement.SetInMission(false);
    }
    private IEnumerator VictoryAnimation()
    {
        victoryCanvas.gameObject.SetActive(true);
        //victory.SetBool("victory",true);
        yield return new WaitForSeconds(5);
        //victory.SetBool("victory",true);
        victoryCanvas.gameObject.SetActive(false);
        messagesCanvas.gameObject.SetActive(false);
        RelativeMovement.SetInMission(false);
    }

    private IEnumerator ReturnToMainCamera(int camera)
    {
        yield return new WaitForSeconds(10);
        mainCamera.gameObject.SetActive(true);
        tutorialCameras[camera].gameObject.SetActive(false);

    }

    private IEnumerator ArtMissionWord()
    {
        messagesCanvas.gameObject.SetActive(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "IN GIRO PER LA STANZA TROVERAI ALCUNE OPERE...";
        mainCamera.gameObject.SetActive(false);
        tutorialCameras[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        tutorialCameras[2].gameObject.SetActive(false);
        tutorialCameras[3].gameObject.SetActive(true);
        //yield return new WaitForSeconds(2);
        textMission.text = "AVVICINANDOTI POTRAI VEDERE CHE IL CARTELLINO DEL NOME E' VUOTO";
        yield return new WaitForSeconds(2);
        textMission.text = "INTERAGISCI E SCRIVI SUL CARTELLINO IL NOME CORRETTO DI TUTTE OPERE PER COMPLETARE LA MISSIONE";
        BambinoControllerAngiolettoMode.talking = false;
        messagesCanvas.gameObject.SetActive(false);
        RelativeMovement.SetInMission(false);
    }

    // Creare un cam Manager
    void Start()
    {
       victory = player.GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void MarkExplanation()
    {
        StartCoroutine(MarkWords());
        StartCoroutine(ReturnToMainCamera(0));
    }

    private void PrefabExplanation()
    {
        StartCoroutine(PrefabMissionWords());
        StartCoroutine(ReturnToMainCamera(1));
    }

    private void TableInteraction()
    {
        textMission.text = "BENE ADESSO CHE HAI TUTTE LE PAROLE VAI AL TAVOLO E FAI I COMPITI";
    }

    private void EndTutorial()
    {
        StartCoroutine(EndTutorialWord());
    }

    private void ArtMission()
    {
        StartCoroutine(ArtMissionWord());
        StartCoroutine(ReturnToMainCamera(3));
    }

    public void ErrorInteraction()
    {
        textMission.text = "NON STARAI DIMENTICANDO QUALCOSA !?";
    }

    public void MissionComplete()
    {
        messagesCanvas.gameObject.SetActive(false);
        StartCoroutine(VictoryAnimation());
    }

    void Awake()
    {
        Messenger.AddListener("PrefabExplanation", PrefabExplanation);
        Messenger.AddListener("MarkExplanation", MarkExplanation);
        Messenger.AddListener("TableInteraction", TableInteraction);
        Messenger.AddListener("EndTutorial", EndTutorial);
        Messenger.AddListener("ArtMission", ArtMission);
        Messenger.AddListener("ErrorInteraction", ErrorInteraction);
        Messenger.AddListener("MissionComplete", MissionComplete);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener("PrefabExplanation", PrefabExplanation);
        Messenger.RemoveListener("MarkExplanation", MarkExplanation);
        Messenger.RemoveListener("TableInteraction", TableInteraction);
        Messenger.RemoveListener("EndTutorial", EndTutorial);
        Messenger.RemoveListener("ArtMission", ArtMission);
        Messenger.RemoveListener("ErrorInteraction", ErrorInteraction);
        Messenger.RemoveListener("MissionComplete", MissionComplete);
    }
}
