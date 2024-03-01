using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuOptions : MonoBehaviour
{

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject scrollPanel;
    [SerializeField] private TMP_Text explanationText;
    [SerializeField] private TMP_Text[] settingText;
    [SerializeField] private GameObject[] closedPanel;
    private string currentSetting = "Collezionabili";
    
    private List<string> addedCollectables = new List<string>();
    private Animator _animator;
    // True se il menu è aperto, false altrimenti 
    private bool _switch = false;
    // La posizione corrente del menu, viene cambiata ogni volta che si entra in un altro tipo di menù
    private string currentMenu = "Collection"; 
    void Start()
    {
        _animator = menu.GetComponent<Animator>();
        
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != GameEvent.START_GAME_SCENE)
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !_switch)
            {
                RelativeMovement.StopMovement(true);
                menu.SetActive(true);
                _switch = true;
            }
            else if (Input.GetKeyUp(KeyCode.Escape) && _switch)
            {
                RelativeMovement.StopMovement(false);
                menu.SetActive(false);
                _switch = false;
            }
        }
    }

    private void InstantiateButton(string name)
    {
        
        GameObject go = Instantiate(buttonPrefab, scrollPanel.transform);
        buttonPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,0f);
        var button = go.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(() => OpenExplanation(name));  
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonText.text = name;  
        addedCollectables.Add(name);
    }
    private void OpenExplanation(string name)
    {
        explanationText.text = ServerComunication.GetInstance().ReciveData(name,"explanation");
        
    }
    private void AddToCollection(Collider _object)
    {
        if(!addedCollectables.Contains(_object.name.ToUpper()))  
            InstantiateButton(_object.name.ToUpper());
    }
    public void OpenCollection()
    {
        Debug.Log("Collection");
        foreach (var i in settingText)
        {
            if (i.name == "CollectionText")
            {
                i.fontStyle = FontStyles.Bold;
                closedPanel[0].SetActive(true);
            }
            else
            {
                i.fontStyle = FontStyles.Normal;
                closedPanel[1].SetActive(false);
                closedPanel[2].SetActive(false);
            }
        } 
    }
    public void OpenControls()
    {
        foreach (var i in settingText)
        {
            if (i.name == "ControlsText")
            {
                i.fontStyle = FontStyles.Bold;
                closedPanel[1].SetActive(true);
            }
            else
            {
                i.fontStyle = FontStyles.Normal;
                closedPanel[0].SetActive(false);
                closedPanel[2].SetActive(false);
            }
        } 
    }
    public void OpenSettings()
    {
        foreach (var i in settingText)
        {
            if (i.name == "SettingsText")
            {
                i.fontStyle = FontStyles.Bold;
                closedPanel[2].SetActive(true);
            }
            else
            {
                i.fontStyle = FontStyles.Normal;
                closedPanel[1].SetActive(false);
                closedPanel[0].SetActive(false);
            }
        } 
    }
    public void Quit()
    {
        Application.Quit();
    }

}
