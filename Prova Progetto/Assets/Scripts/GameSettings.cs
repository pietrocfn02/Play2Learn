using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSettings : MonoBehaviour
{   
    //Parametri che permettono di avere, settare e aggiornare tutte le impostazioni e la grafica dei menu del gioco
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
    
    //Le risoluzioni Supportate dal gioco
    private int [] heights = {1280, 1920,2560,3840};
    private int [] widths = {800, 1080,1440,2160};
    private int pos = 0;
    //Controlla sel il gioco è in pausa
    private bool gameIsPaused = false;
    //Valore per la velocità di movimento del player
    private int speedValue;
    //Valore per l'audio di di gioco
    private float audioValue;
    
    // Metodo che cambia scena 
    public void ChangeScene(){
       SceneManager.LoadScene("Storytelling");
    }

    // Disattiva i pulsanti "Inizia Partita" e "Impostazioni"
    // una volta che si preme sul tasto impostazioni e attiva il Menu del gioco
    public void GoSettings(){
        startGame.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        _exitGame.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(true);
        settingsPanel.SetActive(true);
        
    }
    //Torna nel menu di inizio partita una volta che viene premuto il pulsante nella UI
    //settando su true startGame e settings e su false i restanti 
    public void Menu(){
        if(startGame != null && settings != null){
            startGame.gameObject.SetActive(true);
            settings.gameObject.SetActive(true);
        }
        _exitGame.gameObject.SetActive(true);
        settingsPanel.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }

    //Modifica la risoluzione dello schermo assegnando le posizioni che hanno come indice "size"
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
    //Premendo sul pulsante della UI richiama l'evento, precedentemente assegnato al Button nell'ispector,
    //che esce dal gioco
    public void ExitGame(){
        Application.Quit();
    }
    //Posso eliminarlo ??? 
    public void SetFullscreen(bool _fullScreen){
        Screen.fullScreen = _fullScreen;
    }
    //Premendo sul pulsante della UI richiama l'evento, precedentemente assegnato al Button nell'ispector,
    //e passa alla successiva risoluzione
    public void NextResolution(){
        SetScreenSize(1);
    }

    void Update(){
        //Prendo il nome della scena
        string currentScene = SceneManager.GetActiveScene().name;
        //Controllo se la scena non è la scena iniziale 
        if (currentScene != GameEvent.START_GAME_SCENE){
            //Controllo se il livello della velocità non è null
            if (speedLevel != null){
                //Assegno alla velocita il valore dello slider facendo un cast a intero 
                //per prendere solo la parte intera del numero float
                speedValue = (int) speedSlider.value;
                //Assegno il valore alla UI per visualizzarlo a schermo
                speedLevel.text = speedValue.ToString();
                //Assegno il valore al player "per farlo andare più veloce"
                RelativeMovement.SetMovementSpeed(speedValue);
            }
            //Faccio gli stessi passaggi per il livello dell'audio
            if (audioLevel != null){
                audioValue = audioSlider.value;
                audioLevel.text = audioValue.ToString();
                AudioManager.setAudio(audioValue);
            }
            //Controllo se è stato premuto "esc" sulla tastiera
            /*if (Input.GetKeyDown(KeyCode.Escape)){
                //Assegno i valori della velocita e dell'audio sia al giocatore che alla UI
                speedValue = (int) speedSlider.value;
                audioValue = audioSlider.value;
                speedLevel.text = speedValue.ToString();
                audioLevel.text = audioValue.ToString();
                //Controllo che il gioco sia in pausa 
                if (gameIsPaused){
                    //Richiamo il metodo ResumeGame per riprendere il gioco
                    ResumeGame();
                }else{
                    //Richiamo il metodo PauseGame per mettere in pausa il gioco
                    PauseGame();
                }
            }*/
        }else{
            //Ripeto tutte le cose sopra citate per la scena iniziale, 
            //con la differenza che in quest'ultima non deve essere premuto "esc" per entrare nel menu delle impostazioni
            if (speedLevel != null){
                speedValue = (int) speedSlider.value;
                speedLevel.text = speedValue.ToString();
                RelativeMovement.SetMovementSpeed(speedValue);
            }
            if (audioLevel != null){
                audioValue = audioSlider.value;
                audioLevel.text = audioValue.ToString();
                AudioManager.setAudio(audioValue);
            }
        }
    }
    //Metodo che imposta il valore Time.timeScale = 0 per mettere in pausa il gioco
    public void PauseGame(){
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }
    //Metodo che imposta il valore Time.timeScale = 1 per mettere riprendere il gioco
    public void ResumeGame(){
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}
