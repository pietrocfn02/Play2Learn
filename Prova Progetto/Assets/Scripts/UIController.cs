using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] BambinoController bambinoController;
    [SerializeField] private Text angiolettoScoreText;
    [SerializeField] private Text diavolettoScoreText;


    int angioletto_score;
    int diavoletto_score;

    void Start()
    {
        
    }


    void Awake() {
        Messenger.AddListener(GameEvent.DIAVOLETTO_UPDATE, updateDiavolettoScore );
        Messenger.AddListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore );
    }

    private void updateDiavolettoScore() {
        diavoletto_score = bambinoController.getDiavolettoScore();
        diavolettoScoreText.text = diavoletto_score.ToString();
    }


    private void updateAngiolettoScore() {
        angioletto_score = bambinoController.getAngiolettoScore();
        angiolettoScoreText.text = angioletto_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.DIAVOLETTO_UPDATE, updateDiavolettoScore );
        Messenger.RemoveListener(GameEvent.ANGIOLETTO_UPDATE, updateAngiolettoScore );
    }
}
