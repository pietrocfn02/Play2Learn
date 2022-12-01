using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAngioletto : MonoBehaviour
{
    [SerializeField] BambinoControllerAngiolettoMode bambinoController;
    [SerializeField] private TMP_Text angiolettoScoreText;
    
    
    [SerializeField] private TMP_Text countText; // testo inventario
    [SerializeField] private TMP_Text labelText; // testo movimento
    [SerializeField] private GameObject imageText;  // immagine testo
    [SerializeField] private TMP_Text labelMission; // testo missione
    [SerializeField] private Image inventaryImage; // immagine inventario
    [SerializeField] private Sprite tvImage; // immagine telecomando
    [SerializeField] private Sprite bookImage; // immagine libri
    [SerializeField] private TMP_Text quantityText; // testo quantità

    
    

    int angioletto_score;
    int count = 0;

    void Start(){
    }

    IEnumerator pastelliMission(){
        labelText.text = "";
        imageText.SetActive(true);
        countText.text = "0";
        labelText.text = "COMPLIMENTI HAI FINITO DI RIORDINARE LA STANZA!";
        yield return new WaitForSecondsRealtime(2);
        labelText.text = "MAH?!... NON SENTI ANCHE TU DEI RUMORI?";
        yield return new WaitForSecondsRealtime(2);
        labelText.text = "DEVI AVER LASCIATO LE TV ACCESE!";
        yield return new WaitForSecondsRealtime(2);
        labelText.text = "FORZA! CERCA UN TELECOMANDO E SPEGNILE TUTTE!";
        quantityText.text = "1";
        inventaryImage.sprite = tvImage;
        labelMission.text = "Raccogli un telecomando e spegni le TV.";
        yield return new WaitForSecondsRealtime(2);
        imageText.SetActive(false);
        // aprire la porta quando finisce la missione
        count = 0;
    }

    IEnumerator tvMission(){
        labelText.text = "";
        imageText.SetActive(true);
        countText.text = "0";
        labelText.text = "MA CHE BRAVA, HAI SPENTO TUTTO!";
        yield return new WaitForSecondsRealtime(2);
        labelText.text = "ADESSO MANCA SOLTANTO UNA COSA DA FARE...";
        yield return new WaitForSecondsRealtime(2);
        labelText.text = "E BENE SI... ORA E' ARRIVATO IL MOMENTO DI FARE I COMPITI!!!";
        yield return new WaitForSecondsRealtime(2);
        labelText.text = "ANDIAMO! CERCA IN CASA UN QUADERNO E POI VAI SULLA SCRIVANIA!";
        quantityText.text = "1";
        inventaryImage.sprite = bookImage;
        labelMission.text = "Prendi un quaderno e fai i compiti";
        yield return new WaitForSecondsRealtime(2);
        imageText.SetActive(false);
        // aprire la porta quando finisce la missione
        count = 0;
    }

    void Awake() {
        Messenger.AddListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore);
        Messenger.AddListener(GameEvent.RACCOLTA_UPDATE, updateInventary);
        Messenger.AddListener(GameEvent.LANCIA_OGGETTO, updateInventary);
        Messenger.AddListener(GameEvent.MISSIONE_PASTELLI, completePastelli);
        Messenger.AddListener(GameEvent.MISSIONE_TELEVISIONI, completeTelevisioni);
    }
    
    public void completeTelevisioni(){
        StartCoroutine(tvMission());
    }

    private void updateInventary(){
        count++;
        countText.text = count.ToString();
        if(count == 12){
            imageText.SetActive(true);
            labelText.text="Hai raccolto tutti i pastelli! Ora rimettili a posto nel cestino in mezzo alla stanza!";
        }
    }
    private void completePastelli(){
        StartCoroutine(pastelliMission());
    }
    
    private void updateAngiolettoScore() {
        angioletto_score = bambinoController.getAngiolettoScore();
        angiolettoScoreText.text = angioletto_score.ToString();
    }

    // Update is called once per frame
    void Update(){}

    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore );
        Messenger.RemoveListener(GameEvent.RACCOLTA_UPDATE, updateInventary );
        Messenger.RemoveListener(GameEvent.LANCIA_OGGETTO, updateInventary );
        Messenger.RemoveListener(GameEvent.MISSIONE_PASTELLI, completePastelli);
        Messenger.RemoveListener(GameEvent.MISSIONE_TELEVISIONI, completeTelevisioni);
    }
}
