using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] BambinoController bambinoController;
    //[SerializeField] private Text angiolettoScoreText;
    [SerializeField] private Text diavolettoScoreText;
    //TODO: lista di countText (inventario fisso)
    [SerializeField] private Text telecomandoCountText;
    [SerializeField] private Text pastelliCountText;
    [SerializeField] private Text libriCountText;
    [SerializeField] private TMP_Text labelText;

    int angioletto_score;
    int diavoletto_score;
    ArrayList text_counts = new ArrayList();
    void Start()
    {
        text_counts.Add(this.telecomandoCountText);
        text_counts.Add(this.pastelliCountText);
        text_counts.Add(this.libriCountText);
    }


    void Awake() {
        Messenger.AddListener(GameEvent.DIAVOLETTO_UPDATE, updateDiavolettoScore);
        Messenger.AddListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore);
        Messenger.AddListener(GameEvent.RACCOLTA_UPDATE, updateInventary);
        Messenger.AddListener(GameEvent.LANCIA_OGGETTO, updateInventary);
        Messenger.AddListener(GameEvent.START_TUTORIAL, startTutorial);
        
    }

    private void startTutorial(){
        Debug.Log("Ciaoooo sto startando il tutorial");
        labelText.text="Per iniziare raccogli i pastelli sul letto!";
    }

    private void updateInventary(){
        for(int i=0; i<text_counts.Count; i++)
        {
            text_counts[i] = bambinoController.getOggettoCount(i);
            if(i==0)
                telecomandoCountText.text = text_counts[i].ToString();
            else if(i==1)
                pastelliCountText.text = text_counts[i].ToString();
            else if(i==2)
                libriCountText.text = text_counts[i].ToString();
        }
        labelText.text="Hai raccolto i pastelli! Mettili al fresco in frigo!";
    }

    

    private void updateDiavolettoScore() {
        diavoletto_score = bambinoController.getDiavolettoScore();
        diavolettoScoreText.text = diavoletto_score.ToString();
    }


    private void updateAngiolettoScore() {
        angioletto_score = bambinoController.getAngiolettoScore();
        //angiolettoScoreText.text = angioletto_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.DIAVOLETTO_UPDATE, updateDiavolettoScore );
        Messenger.RemoveListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore );
        Messenger.RemoveListener(GameEvent.RACCOLTA_UPDATE, updateInventary );
        Messenger.RemoveListener(GameEvent.LANCIA_OGGETTO, updateInventary );
    }
}
