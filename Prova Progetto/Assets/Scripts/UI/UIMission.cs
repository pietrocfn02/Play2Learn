using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMission : MonoBehaviour
{
    [SerializeField] private Canvas italianMission;
    [SerializeField] private Canvas artMission;
    [SerializeField] private GameObject[] artPrefabs;
    private string tag = "";
    private bool mission = false;
    void Start()
    {
    }

    void Update()
    {
        if (!mission)
            ArtMission();
    }

    public void ItalianMission()
    {
        Time.timeScale = 0;
        italianMission.gameObject.SetActive(true);
    }

    IEnumerator timer(){
        yield return new WaitForSecondsRealtime(10);
    }
    public void ArtMission()
    {
        Debug.Log(tag);
        switch(tag)
        {
            case GameEvent.VITRUVIAN_TAG:
                break;
            case GameEvent.COLUMN_CORINTHIAN_TAG:
                GameObject tmp = GameObject.Find("CorinthianCamera");
                GameObject[] mainCamera = GameObject.FindGameObjectsWithTag("MainCamera");
                for(int i=0;i<mainCamera.Length;++i){
                    mainCamera[i].SetActive(false);
                }
                artMission.gameObject.SetActive(true);
                
                StartCoroutine(timer());

                mission = true;
                for(int i=0;i<mainCamera.Length;++i){
                    mainCamera[i].SetActive(true);
                }
                artMission.gameObject.SetActive(false);
                Time.timeScale = 1;
                break;
            case GameEvent.COLUMN_IONIC_TAG:
                Debug.Log(tag);
                break;
            case GameEvent.TOPOLINO_TAG:
                Debug.Log(tag);
                break;
            case GameEvent.ONEPIECE_TAG:
                Debug.Log(tag);
                break;
            case GameEvent.SNOOPY_TAG:
                Debug.Log(tag);
                break;
            case GameEvent.SUPERMAN_TAG:
                Debug.Log(tag);
                break;
            default:
                mission = false;
                break;    
        }
    }
    

    private void SetVitruvian()
    {
        tag = GameEvent.VITRUVIAN_TAG;
    }
    private void SetCorinthian()
    {
        tag = GameEvent.COLUMN_CORINTHIAN_TAG;
    }
    private void SetIonic()
    {
        tag = GameEvent.COLUMN_IONIC_TAG;
    }
    private void SetTopolino()
    {
        tag = GameEvent.TOPOLINO_TAG;
    }
    private void SetOnePiece()
    {
        tag = GameEvent.ONEPIECE_TAG;
    }
    private void SetSnoopy()
    {
        tag = GameEvent.SNOOPY_TAG;
    }
    private void SetSuperMan()
    {
        tag = GameEvent.SUPERMAN_TAG;
    }
    void onDestroy()
    {
        Messenger.RemoveListener(GameEvent.FIRST_UI_MISSION, ItalianMission);
        Messenger.RemoveListener(GameEvent.VITRUVIAN_TAG, SetVitruvian);
        Messenger.RemoveListener(GameEvent.COLUMN_CORINTHIAN_TAG, SetCorinthian);
        Messenger.RemoveListener(GameEvent.COLUMN_IONIC_TAG, SetIonic);
        Messenger.RemoveListener(GameEvent.TOPOLINO_TAG, SetTopolino);
        Messenger.RemoveListener(GameEvent.ONEPIECE_TAG, SetOnePiece);
        Messenger.RemoveListener(GameEvent.SNOOPY_TAG, SetSnoopy);
        Messenger.RemoveListener(GameEvent.SUPERMAN_TAG, SetSuperMan);
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.FIRST_UI_MISSION, ItalianMission);
        Messenger.AddListener(GameEvent.VITRUVIAN_TAG, SetVitruvian);
        Messenger.AddListener(GameEvent.COLUMN_CORINTHIAN_TAG, SetCorinthian);
        Messenger.AddListener(GameEvent.COLUMN_IONIC_TAG, SetIonic);
        Messenger.AddListener(GameEvent.TOPOLINO_TAG, SetTopolino);
        Messenger.AddListener(GameEvent.ONEPIECE_TAG, SetOnePiece);
        Messenger.AddListener(GameEvent.SNOOPY_TAG, SetSnoopy);
        Messenger.AddListener(GameEvent.SUPERMAN_TAG, SetSuperMan);
    }
}
