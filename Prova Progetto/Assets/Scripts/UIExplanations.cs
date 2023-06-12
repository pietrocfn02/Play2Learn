using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
public class UIExplanations : MonoBehaviour
{

    [SerializeField] private Image messagesContainer;
    [SerializeField] private TMP_Text textMission;
    [SerializeField] private Canvas victoryCanvas;
    [SerializeField] private CinemachineVirtualCamera[] explanationCameras;
    [SerializeField] private CinemachineFreeLook mainCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private Image activeMission;
    [SerializeField] private TMP_Text activeMissionText;
    
    private Animator victory;

    private IEnumerator MarkWords()
    {
        RelativeMovement.SetInMission(true);
        messagesContainer.gameObject.SetActive(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "GUARDA, ALLA TUA DESTRA E' APPARSO QUALCOSA...";
        yield return new WaitForSeconds(2);
        mainCamera.gameObject.SetActive(false);
        explanationCameras[0].gameObject.SetActive(true);
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
        BambinoControllerAngiolettoMode.talking = false;
        
    }

    private IEnumerator PrefabMissionWords()
    {
        RelativeMovement.SetInMission(true);
        messagesContainer.gameObject.SetActive(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "UNA VOLTA PREMUTO -E- COMPARIRANNO DEI SIMBOLI SOTTO L'OGGETTO INTERAGIBILE.";
        yield return new WaitForSeconds(2);
        mainCamera.gameObject.SetActive(false);
        explanationCameras[0].gameObject.SetActive(true);
        textMission.text = "COME PUOI VEDERE OGNUNO DI QUESTI SIMBOLI SBLOCCA UNA MISSIONE E";
        yield return new WaitForSeconds(2);
        explanationCameras[0].gameObject.SetActive(false);
        explanationCameras[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        textMission.text = "UNA VOLTA FINITA LA MISSIONE TI VERRANNO SBLOCCATI ALTRI OGGETTI IN GIRO PER LA MAPPA CHE HANNO QUALCOSA DA RACCONTARE,";
        yield return new WaitForSeconds(2);
        textMission.text = "BASTERA' SOLTANTO AVVICINARTI E PREMERE -E-";
        yield return new WaitForSeconds(5);
        BambinoControllerAngiolettoMode.talking = false;
        
    }

    private IEnumerator EndTutorialWord()
    {
        RelativeMovement.SetInMission(true);
        messagesContainer.gameObject.SetActive(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "BENE!!! HAI COMPLETATO CORRETTAMENTE IL TUTORIAL!";
        yield return new WaitForSeconds(2);
        textMission.text = "ADESSO NON TI RIMANE CHE GIROVAGARE PER CASA E IMPARARE.";
        yield return new WaitForSeconds(2);
        BambinoControllerAngiolettoMode.talking = false;
        
    }
    private IEnumerator VictoryAnimation()
    {
        victoryCanvas.gameObject.SetActive(true);
        //victory.SetBool("victory",true);
        yield return new WaitForSeconds(5);
        //victory.SetBool("victory",true);
        victoryCanvas.gameObject.SetActive(false);
    }

    private IEnumerator ReturnToMainCamera(int camera, int timer)
    {
        yield return new WaitForSeconds(timer);
        textMission.text = "";
        mainCamera.gameObject.SetActive(true);
        explanationCameras[camera].gameObject.SetActive(false);
        messagesContainer.gameObject.SetActive(false);
        
    }

    private IEnumerator ArtMissionWord()
    {
        messagesContainer.gameObject.SetActive(true);
        RelativeMovement.SetInMission(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "IN GIRO PER LA STANZA TROVERAI ALCUNE OPERE...";
        mainCamera.gameObject.SetActive(false);
        explanationCameras[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        textMission.text = "AVVICINANDOTI POTRAI VEDERE CHE IL CARTELLINO DEL NOME E' VUOTO";
        explanationCameras[2].gameObject.SetActive(false);
        explanationCameras[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        textMission.text = "INTERAGISCI E SCRIVI SUL CARTELLINO IL NOME CORRETTO DI TUTTE OPERE PER COMPLETARE LA MISSIONE";
        
        BambinoControllerAngiolettoMode.talking = false;
    }
    private IEnumerator Timer(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
    }

    private IEnumerator MathMissionWord()
    {
        messagesContainer.gameObject.SetActive(true);
        RelativeMovement.SetInMission(true);
        BambinoControllerAngiolettoMode.talking = true;
        textMission.text = "BENVENUTO NELLA MISSIONE DI MATEMATICA...";
        yield return new WaitForSeconds(2);
        mainCamera.gameObject.SetActive(false);
        explanationCameras[4].gameObject.SetActive(true);
        textMission.text = "COME PRIMA COSA VAI VERSO IL PROGETTO...";
        yield return new WaitForSeconds(2);
        textMission.text = "LO TROVI POGIATO SULLE PEDANE DI LEGNO.";
        BambinoControllerAngiolettoMode.talking = false;
        
    }

    private IEnumerator MathMissionWord2()
    {
        messagesContainer.gameObject.SetActive(true);
        RelativeMovement.SetInMission(true);
        textMission.text = "COME PUOI VEDERE, QUESTA E' UNA STANZA IN COSTRUZIONE...";
        BambinoControllerAngiolettoMode.talking = true;
        mainCamera.gameObject.SetActive(false);
        explanationCameras[5].gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textMission.text = "PURTROPPO DEI LAVORATORI SFATICATI NON SONO ANDATI AVANTI CON I LAVORI.";
        explanationCameras[5].gameObject.SetActive(false);
        explanationCameras[6].gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textMission.text = "PERCHE' NON PROVI TU A COMPLETARLI CON LE TUE CONOSCENZE DI MATEMATICA E GEOMETRIA?";
        explanationCameras[6].gameObject.SetActive(false);
        explanationCameras[7].gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textMission.text = "INIZIA AD ANDARE A PRENDERE I CONI PER POSIZIONARLI NEI QUATTRO ANGOLI DELLA STANZA.";
        explanationCameras[7].gameObject.SetActive(false);
        explanationCameras[8].gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textMission.text = "LI TROVI ALLE TUE SPALLE.";
        yield return new WaitForSeconds(3);
        activeMission.gameObject.SetActive(true);
        activeMissionText.text = "PRENDI I CONI.";
        BambinoControllerAngiolettoMode.talking = false;
        
    }

    private IEnumerator MathMissionWord4()
    {
       RelativeMovement.SetInMission(true);
        BambinoControllerAngiolettoMode.talking = true;
        activeMissionText.text = "POSIZIONA I CONI.";
        messagesContainer.gameObject.SetActive(true);
        textMission.text = "OTTIMO ORA POSIZIONALI NEI QUATTRO ANGOLI DELLA STANZA.";
        yield return new WaitForSeconds(4);
        messagesContainer.gameObject.SetActive(false);
        BambinoControllerAngiolettoMode.talking = false;
        
    }

    private IEnumerator MathMissionWord3()
    {
        RelativeMovement.SetInMission(true);
        BambinoControllerAngiolettoMode.talking = true;
        messagesContainer.gameObject.SetActive(true);
        activeMission.gameObject.SetActive(false);
        textMission.text = "COMPLIMENTI!";
        yield return new WaitForSeconds(2);
        textMission.text = "HAI POSIZIONATO I CONI IN TUTTI GLI ANGOLI DELLA STANZA!";
        mainCamera.gameObject.SetActive(false);
        explanationCameras[5].gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textMission.text = "ADESSO VAI A PRENDERE IL NASTRO GIALLO E METTI IN SICUREZZA LA STANZA...";
        yield return new WaitForSeconds(2);
        textMission.text = "AVVICINATI E ATTACCA IL NASTRO AI CONI PER DELIMITARNE IL PERIMETRO ";
        activeMission.gameObject.SetActive(true);
        activeMissionText.text = "PRENDI IL NASTRO";
        BambinoControllerAngiolettoMode.talking = true;
        
    }
    
    private IEnumerator MathMissionWord6()
    {
        BambinoControllerAngiolettoMode.talking = true;
        RelativeMovement.SetInMission(true);
        messagesContainer.gameObject.SetActive(true);
        activeMission.gameObject.SetActive(false);
        textMission.text = "UN BUON PROGETTO NON PUO ESSERE PRIVO DI UN PERIMETRO E DI UN AREA...";
        yield return new WaitForSeconds(2);
        textMission.text = "PRENDI LA CALCOLATRICE E INTERAGISCI CON IL PROGETTO PER CALCOLARLI!";
        mainCamera.gameObject.SetActive(false);
        explanationCameras[9].gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textMission.text = "E' PROPIO LI PER TERRA, COSA ASPETTI.";
        activeMission.gameObject.SetActive(true);
        activeMissionText.text = "PRENDI LA CALCOLATRICE";
        BambinoControllerAngiolettoMode.talking = true;
        
    }

    private IEnumerator MathMissionWord7()
    {
        RelativeMovement.SetInMission(true);
        BambinoControllerAngiolettoMode.talking = true;
        activeMissionText.text = "VAI AL PROGETTO";
        messagesContainer.gameObject.SetActive(true);
        textMission.text = "PERFETTO! ORA VAI AL PROGETTO ";
        yield return new WaitForSeconds(4);
        messagesContainer.gameObject.SetActive(false);
        BambinoControllerAngiolettoMode.talking = false;
        
    }

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
        StartCoroutine(ReturnToMainCamera(0,10));
    }

    private void PrefabExplanation()
    {
        StartCoroutine(PrefabMissionWords());
        StartCoroutine(ReturnToMainCamera(1,10));
    }

    private void TableInteraction()
    {
        messagesContainer.gameObject.SetActive(true);
        textMission.text = "BENE ADESSO CHE HAI TUTTE LE PAROLE VAI AL TAVOLO E FAI I COMPITI";
        StartCoroutine(Timer(2));
        messagesContainer.gameObject.SetActive(false);
    }

    private void EndTutorial()
    {
        StartCoroutine(EndTutorialWord());
    }

    private void ArtMission()
    {
        StartCoroutine(ArtMissionWord());
        StartCoroutine(ReturnToMainCamera(3,10));
    }

    private void MathMission()
    {
        StartCoroutine(MathMissionWord());
        StartCoroutine(ReturnToMainCamera(4,8));
    }

    private void MathMission2()
    {
        StartCoroutine(MathMissionWord2());
        StartCoroutine(ReturnToMainCamera(8,15));
    }

    
    private void MathMission3()
    {
        StartCoroutine(MathMissionWord4());
    }

    private void MathMission4()
    {
        StartCoroutine(MathMissionWord3());
        StartCoroutine(ReturnToMainCamera(5,10));
    }
    
    private void MathMission5()
    {
        activeMissionText.text = "ATTACCA IL NASTRO AI CONI ADIACENTI";
    }

    private void MathMission6()
    {
        StartCoroutine(MathMissionWord6());
        StartCoroutine(ReturnToMainCamera(9,7));
    }
    private void MathMission7()
    {
        StartCoroutine(MathMissionWord7());
    }

    public void ErrorInteraction()
    {
        textMission.text = "NON STARAI DIMENTICANDO QUALCOSA !?";
    }

    public void MissionComplete()
    {
        messagesContainer.gameObject.SetActive(false);
        StartCoroutine(VictoryAnimation());
    }

    void Awake()
    {
        Messenger.AddListener("PrefabExplanation", PrefabExplanation);
        Messenger.AddListener("MarkExplanation", MarkExplanation);
        Messenger.AddListener("TableInteraction", TableInteraction);
        Messenger.AddListener("EndTutorial", EndTutorial);
        Messenger.AddListener("ArtMission", ArtMission);
        Messenger.AddListener("MathMission", MathMission);
        Messenger.AddListener("MathMission2", MathMission2);
        Messenger.AddListener("MathMission3", MathMission3);
        Messenger.AddListener("MathMission4", MathMission4);
        Messenger.AddListener("MathMission5", MathMission5);
        Messenger.AddListener("MathMission6", MathMission6);
        Messenger.AddListener("MathMission7", MathMission7);
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
        Messenger.RemoveListener("MathMission", MathMission);
        Messenger.RemoveListener("MathMission2", MathMission2);
        Messenger.RemoveListener("MathMission3", MathMission3);
        Messenger.RemoveListener("MathMission5", MathMission5);
        Messenger.RemoveListener("MathMission6", MathMission6);
        Messenger.RemoveListener("MathMission7", MathMission7);
        Messenger.RemoveListener("ErrorInteraction", ErrorInteraction);
        Messenger.RemoveListener("MissionComplete", MissionComplete);
    }
}
