using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAngioletto : MonoBehaviour
{

    // UI Controller della angioletto mode

    
    [SerializeField] BambinoControllerAngiolettoMode bambinoController;
    [SerializeField] private TMP_Text angiolettoScoreText;
    
    
    [SerializeField] private TMP_Text countText; // testo inventario
    [SerializeField] private TMP_Text labelText; // testo messaggi
    [SerializeField] private GameObject imageText;  // immagine messaggi
    [SerializeField] private TMP_Text labelMission; // testo missione
    [SerializeField] private Image inventaryImage; // immagine inventario
    [SerializeField] private Sprite tvImage; // immagine telecomando
    [SerializeField] private Sprite bookImage; // immagine libri
    [SerializeField] private TMP_Text quantityText; // testo quantità
    [SerializeField] private GameObject homeworkImage; // immagine foglio
    
    

    int angioletto_score;
    int count = 0;

    void Start(){
    }
    // Coroutine per la fine della prima missione in angioletto mode
    IEnumerator pastelliMission(){
        labelText.text = UIMessages.EMPTY_MESSAGE;
        imageText.SetActive(true);
        countText.text = UIMessages.ZERO;
        labelText.text = UIMessages.PASTELLI_1;
        yield return new WaitForSecondsRealtime(2);
        labelText.text = UIMessages.PASTELLI_2;
        yield return new WaitForSecondsRealtime(2);
        labelText.text = UIMessages.PASTELLI_3;
        yield return new WaitForSecondsRealtime(2);
        labelText.text = UIMessages.PASTELLI_4;
        quantityText.text = UIMessages.ONE;
        inventaryImage.sprite = tvImage;
        labelMission.text = UIMessages.PASTELLI_5;
        yield return new WaitForSecondsRealtime(2);
        imageText.SetActive(false);
        count = 0;
    }
    // Coroutine per la fine della prima missione in angioletto mode
    IEnumerator tvMission(){
        labelText.text = UIMessages.EMPTY_MESSAGE;
        imageText.SetActive(true);
        countText.text = UIMessages.ZERO;
        labelText.text = UIMessages.TV_1;
        yield return new WaitForSecondsRealtime(2);
        labelText.text = UIMessages.TV_2;
        yield return new WaitForSecondsRealtime(2);
        labelText.text = UIMessages.TV_3;
        yield return new WaitForSecondsRealtime(2);
        labelText.text = UIMessages.TV_4;
        quantityText.text = UIMessages.ONE;
        inventaryImage.sprite = bookImage;
        labelMission.text = UIMessages.TV_5;
        yield return new WaitForSecondsRealtime(2);
        imageText.SetActive(false);
        count = 0;
    }
    // Coroutine per la chiusura dei messaggi temporanei
    IEnumerator closeMessage(string s){
        labelText.text = s;
        imageText.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        imageText.SetActive(false);

    }

    // ADD LISTENER
    void Awake() {
        Messenger.AddListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore);
        Messenger.AddListener(GameEvent.RACCOLTA_UPDATE, updateInventary);
        Messenger.AddListener(GameEvent.LANCIA_OGGETTO, updateInventary);
        Messenger.AddListener(GameEvent.MISSIONE_PASTELLI, completePastelli);
        Messenger.AddListener(GameEvent.MISSIONE_TELEVISIONI, completeTelevisioni);
        Messenger.AddListener(GameEvent.MISSIONE_COMPITI, doHomework);
        Messenger.AddListener(GameEvent.FANTASMINO_EVENTO, youShallNotPass);
        Messenger.AddListener(GameEvent.FORGET, forgetText);
    }

    // attiva il canvas dei compiti e disattiva gli altri 
    public void forgetText(){
        StartCoroutine(closeMessage(UIMessages.FORGET));
    }
    // Spawn del foglio per i compiti
    public void doHomework(){
        homeworkImage.SetActive(true);
    }    
    // Spawn del messaggio ... nella UI del fantasma che blocca la porta nella prima missioe
    public void youShallNotPass(){
        StartCoroutine(closeMessage(UIMessages.YOU_SHELL_NOT_PASS));

    }
    // Start della coroutine per l'inizio della seconda missione
    public void completeTelevisioni(){
        StartCoroutine(tvMission());
    }
    // Aggiorna l'inventario (UI)
    private void updateInventary(){
        count++;
        countText.text = count.ToString();
        if(count == 12){
            imageText.SetActive(true);
            labelText.text = UIMessages.END_FIRST_GOOD_ACTION;
        }
    }
    // Start della coroutine per l'inizio della missione finale
    private void completePastelli(){
        StartCoroutine(pastelliMission());
    }
    
    private void updateAngiolettoScore() {
        angioletto_score = bambinoController.getAngiolettoScore();
        angiolettoScoreText.text = angioletto_score.ToString();
    }

    void Update(){}
    // REMOVE LISTENER
    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore );
        Messenger.RemoveListener(GameEvent.RACCOLTA_UPDATE, updateInventary );
        Messenger.RemoveListener(GameEvent.LANCIA_OGGETTO, updateInventary );
        Messenger.RemoveListener(GameEvent.MISSIONE_PASTELLI, completePastelli);
        Messenger.RemoveListener(GameEvent.MISSIONE_TELEVISIONI, completeTelevisioni);
        Messenger.RemoveListener(GameEvent.MISSIONE_COMPITI, doHomework);
        Messenger.RemoveListener(GameEvent.FANTASMINO_EVENTO, youShallNotPass);
        Messenger.RemoveListener(GameEvent.FORGET, forgetText);
    }
}
