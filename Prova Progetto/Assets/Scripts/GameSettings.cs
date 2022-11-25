using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSettings : MonoBehaviour
{   

    [SerializeField] private Button startGame;
    [SerializeField] private Button settings;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] public Slider audioSlider;
    [SerializeField] public Slider speedSlider;
    [SerializeField] public Toggle tutorialToggle; 
    [SerializeField] public TMP_Text audioLevel;
    [SerializeField] public TMP_Text speedLevel;
    [SerializeField] public Button menuButton;
    
    public void Start(){
        startGame.gameObject.SetActive(true);
        settings.gameObject.SetActive(true);
        settingsPanel.SetActive(false);
        menuButton.gameObject.SetActive(false);
        speedLevel.text = speedSlider.value.ToString();
        audioLevel.text = audioSlider.value.ToString();
    }

    // Metodo che cambia scena 
    public void ChangeScene(){
        SceneManager.LoadScene("Diavoletto_Scene");
    }

    // Disattiva i pulsanti "inizia Partita" e "Impostazioni"
    // una volta che si preme sul tasto impostazioni
    public void GoSettings(){
        startGame.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        settingsPanel.SetActive(true);
    }

    // Modifica le impostazioni del gioco
    public void ChangeSettings(){
        speedLevel.text = speedSlider.value.ToString();
        audioLevel.text = audioSlider.value.ToString();
        //Debug.Log("#audio value:" + audioSlider.value + "#########");
        //Debug.Log("#speed value:" + speedSlider.value + "#########");
    }

    public void menu(){
        startGame.gameObject.SetActive(true);
        settings.gameObject.SetActive(true);
        settingsPanel.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }

    // Serve per attivare o disattivare il tutorial 
    public void Tutorial(){
        if (tutorialToggle.isOn){
            // attivare tutorial
            Debug.Log("##### ATTIVO #####");
        }else{
            // disattivare tutorial
            Debug.Log("##### DISATTIVO #####");
        }
    }
}
