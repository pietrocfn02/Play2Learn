using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    // Ui controller della diavoletto mode


    [SerializeField] BambinoController bambinoController;
    [SerializeField] private TMP_Text diavolettoScoreText;
    [SerializeField] private TMP_Text telecomandoCountText;
    [SerializeField] private TMP_Text pastelliCountText;
    [SerializeField] private TMP_Text libriCountText;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private GameObject imageText;

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
        Messenger.AddListener(GameEvent.RACCOLTA_UPDATE, updateInventary);
        Messenger.AddListener(GameEvent.LANCIA_OGGETTO, updateInventary);
        Messenger.AddListener(GameEvent.START_TUTORIAL, startTutorial);
        Messenger.AddListener(GameEvent.MISSION_COMPLETE, missionComplete);
        Messenger.AddListener(GameEvent.FIRST_MISSION_COMPLETE, missionComplete);        
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
        labelText.text = UIMessages.EMPTY_MESSAGE;
    }
    
    private void missionComplete(){
        labelText.text ="";
        labelText.text = UIMessages.END_MARACHELLA;
        imageText.SetActive(true);
    }


    void Update()
    {
        // Tolgo tutto il disegnino quando non ho nessun testo da mostrare
        if(labelText.text=="" || labelText.text == UIMessages.EMPTY_MESSAGE)
        {
            imageText.SetActive(false);
        } else {
            imageText.SetActive(true);
        }
    }


    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.DIAVOLETTO_UPDATE, updateDiavolettoScore );
        Messenger.RemoveListener(GameEvent.RACCOLTA_UPDATE, updateInventary );
        Messenger.RemoveListener(GameEvent.LANCIA_OGGETTO, updateInventary );
        Messenger.RemoveListener(GameEvent.MISSION_COMPLETE, missionComplete);
        Messenger.RemoveListener(GameEvent.FIRST_MISSION_COMPLETE, missionComplete);
    }
}
