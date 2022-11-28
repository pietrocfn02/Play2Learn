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
    [SerializeField] public Button nextResolutionButton;
    [SerializeField] public TMP_Text resolutionText;
    
    private int [] heights = {1280, 1920,2048,3840};
    private int [] widths = {800, 1080,1080,2160};
    private int pos = 0;

    public void Start(){
        resolutionText.text = "Risoluzione";
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


    public void SetScreenSize(int size){
        if (pos != widths.Length-1){
            pos += size;
        }else{
            pos = 0;
        }
        
        bool fullscreen = Screen.fullScreen;
        int width = widths[pos];
        int height = heights[pos];
        //Debug.Log(height + "x" + width);
        resolutionText.text = height + "x" + width;
        Screen.SetResolution(width,height,fullscreen);
    }

    public void SetFullscreen(bool _fullScreen){
        Screen.fullScreen = _fullScreen;
    }
    public void nextResolution(){
        SetScreenSize(1);
    }
}
