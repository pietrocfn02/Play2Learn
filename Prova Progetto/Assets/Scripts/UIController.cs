using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] BambinoController bambinoController;
    //[SerializeField] private Text angiolettoScoreText;
    [SerializeField] private TMP_Text diavolettoScoreText;
    //TODO: lista di countText (inventario fisso)
    [SerializeField] private TMP_Text telecomandoCountText;
    [SerializeField] private TMP_Text pastelliCountText;
    [SerializeField] private TMP_Text libriCountText;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private GameObject imageText;

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
        Messenger.AddListener(GameEvent.MISSION_COMPLETE, missionComplete);
        Messenger.AddListener(GameEvent.FIRST_MISSION_COMPLETE, firstMissionComplete);        
    }

    private void startTutorial(){
        labelText.text = UIMessages.START_TUTORIAL_MESSAGE;
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
    }


    private void updateDiavolettoScore() {
        diavoletto_score = bambinoController.getDiavolettoScore();
        diavolettoScoreText.text = diavoletto_score.ToString();
        labelText.text=UIMessages.EMPTY_MESSAGE;
    }


    private void updateAngiolettoScore() {
        angioletto_score = bambinoController.getAngiolettoScore();
        //angiolettoScoreText.text = angioletto_score.ToString();
    }

    private void missionComplete(){
        labelText.text = UIMessages.END_MARACHELLA;
        imageText.SetActive(true);
    }

    private void firstMissionComplete(){
        labelText.text = UIMessages.FINE_PRIMA_MARACHELLA;
        imageText.SetActive(true);    
    }

    // Update is called once per frame
    void Update()
    {
        if(labelText.text=="" || labelText.text == UIMessages.EMPTY_MESSAGE)
        {
            imageText.SetActive(false);
        } else {
            imageText.SetActive(true);
        }
    }


    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.DIAVOLETTO_UPDATE, updateDiavolettoScore );
        Messenger.RemoveListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore );
        Messenger.RemoveListener(GameEvent.RACCOLTA_UPDATE, updateInventary );
        Messenger.RemoveListener(GameEvent.LANCIA_OGGETTO, updateInventary );
        Messenger.RemoveListener(GameEvent.MISSION_COMPLETE, missionComplete);
        Messenger.RemoveListener(GameEvent.FIRST_MISSION_COMPLETE, firstMissionComplete);
    }
}
