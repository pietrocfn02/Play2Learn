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
    [SerializeField] public TMP_Text audioLevel;
    [SerializeField] public TMP_Text speedLevel;
    [SerializeField] public Button menuButton;
    [SerializeField] public Button nextResolutionButton;
    [SerializeField] public TMP_Text resolutionText;
    [SerializeField] public Button _exitGame;
    
    private int [] heights = {1280, 1920,2560,3840};
    private int [] widths = {800, 1080,1440,2160};
    private int pos = 0;
    private bool gameIsPaused = false;
    private static int speedValue;
    private static int audioValue;
    
    // Metodo che cambia scena 
    public void ChangeScene(){
       SceneManager.LoadScene("Storytelling");
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
    public void Menu(){
        if(startGame != null && settings != null){
            startGame.gameObject.SetActive(true);
            settings.gameObject.SetActive(true);
        }
        _exitGame.gameObject.SetActive(true);
        settingsPanel.SetActive(false);
        menuButton.gameObject.SetActive(false);
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
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != GameEvent.START_GAME_SCENE){
            if (speedLevel != null){
                speedValue = (int) speedSlider.value;
                speedLevel.text = speedValue.ToString();
                RelativeMovement.SetMovementSpeed(speedValue);
            }
            if (audioLevel != null){
                audioValue = (int) audioSlider.value;
                audioLevel.text = audioValue.ToString();
            }
            if (Input.GetKeyDown(KeyCode.Escape)){
                speedValue = (int) speedSlider.value;
                audioValue = (int) audioSlider.value;
                speedLevel.text = speedValue.ToString();
                audioLevel.text = audioValue.ToString();
                if (gameIsPaused){
                    ResumeGame();
                }else{
                    PauseGame();
                }
            }
        }else{
            if (speedLevel != null){
                speedValue = (int) speedSlider.value;
                speedLevel.text = speedValue.ToString();
                RelativeMovement.SetMovementSpeed(speedValue);
            }
            if (audioLevel != null){
                audioValue = (int) audioSlider.value;
                audioLevel.text = audioValue.ToString();
            }
        }
    }
    
    public void PauseGame(){
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void ResumeGame(){
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}
