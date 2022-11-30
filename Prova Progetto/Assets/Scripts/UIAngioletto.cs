using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAngioletto : MonoBehaviour
{
    [SerializeField] BambinoControllerAngiolettoMode bambinoController;
    [SerializeField] private TMP_Text angiolettoScoreText;
    
    
    [SerializeField] private TMP_Text pastelliCountText;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private GameObject imageText;

    int angioletto_score;
    int count = 0;
    void Start()
    {
    }


    void Awake() {
        Messenger.AddListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore);
        Messenger.AddListener(GameEvent.RACCOLTA_UPDATE, updateInventary);
        Messenger.AddListener(GameEvent.LANCIA_OGGETTO, updateInventary);
        
    }
    
    private void updateInventary(){
        count++;
        pastelliCountText.text = count.ToString();
        if(count == 12){
            labelText.text="Hai raccolto tutti i pastelli! Ora rimettili a posto nel cestino in mezzo alla stanza!";
        }
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
    }
}
