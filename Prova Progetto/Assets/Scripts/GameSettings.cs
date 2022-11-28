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
    [SerializeField] public Button _exitGame;
    
    private int [] heights = {1280, 1920,2048,3840};
    private int [] widths = {800, 1080,1080,2160};
    private int pos = 0;
    private bool gameIsPaused = false;

    public void Start(){
        resolutionText.text = "Risoluzione";
        startGame.gameObject.SetActive(true);
        _exitGame.gameObject.SetActive(true);
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
        _exitGame.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        settingsPanel.SetActive(true);
    }

    // Modifica le impostazioni del gioco
    public void ChangeSettings(){
        speedLevel.text = speedSlider.value.ToString();
        audioLevel.text = audioSlider.value.ToString();
    }

    public void Menu(){
        startGame.gameObject.SetActive(true);
        settings.gameObject.SetActive(true);
        _exitGame.gameObject.SetActive(true);
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
        resolutionText.text = height + "x" + width;
        Screen.SetResolution(height,width,fullscreen);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void SetAudio(){
        // Manda un messaggio broadcast per settare l'audio del gioco
        //potrebbe non servire mettendo un serialized field dell'audio
    }

    public void SetSpeed(){
        // Manda un messaggio broadcast per settare la velocità del player
    }
    public void SetFullscreen(bool _fullScreen){
        Screen.fullScreen = _fullScreen;
    }
    public void NextResolution(){
        SetScreenSize(1);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (gameIsPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        Debug.Log("Stop");
    }

    public void ResumeGame(){
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        Debug.Log("Go");
    }
}
