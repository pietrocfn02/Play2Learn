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
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private GameObject imageText;
    [SerializeField] private TMP_Text labelMission; // testo missione
    [SerializeField] private Image inventaryImage; // immagine inventario
    [SerializeField] private Sprite remoteImage; // immagine telecomando
    
    

    int angioletto_score;
    int count = 0;

    void Start()
    {
    }


    void Awake() {
        Messenger.AddListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore);
        Messenger.AddListener(GameEvent.RACCOLTA_UPDATE, updateInventary);
        Messenger.AddListener(GameEvent.LANCIA_OGGETTO, updateInventary);
        Messenger.AddListener(GameEvent.MISSIONE_PASTELLI, completePastelli);
    }
    
    private void updateInventary(){
        count++;
        countText.text = count.ToString();
        if(count == 12){
            labelText.text="Hai raccolto tutti i pastelli! Ora rimettili a posto nel cestino in mezzo alla stanza!";
        }
    }
    
    private void completePastelli(){
        countText.text = "0";
        labelText.text = "BUONA AZIONE COMPLETATA! BENE ORA PASSA ALLA PROSSIMA!";
        inventaryImage.sprite = remoteImage;
        labelMission.text = "Raccogli un telecomando e spegni le TV.";
    }
    
    private void updateAngiolettoScore() {
        angioletto_score = bambinoController.getAngiolettoScore();
        angiolettoScoreText.text = angioletto_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(labelText.text =="")
        {
            imageText.SetActive(false);
        } else {
            imageText.SetActive(true);
        }
    }


    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore );
        Messenger.RemoveListener(GameEvent.RACCOLTA_UPDATE, updateInventary );
        Messenger.RemoveListener(GameEvent.LANCIA_OGGETTO, updateInventary );
        Messenger.RemoveListener(GameEvent.MISSIONE_PASTELLI, completePastelli);
    }
}
