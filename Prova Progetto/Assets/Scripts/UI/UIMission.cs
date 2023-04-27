using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class UIMission : MonoBehaviour
{
    [SerializeField] private Canvas italianMission;
    [SerializeField] private Canvas artMission;
    [SerializeField] private TMP_Text artText;
    [SerializeField] private GameObject[] artPrefabs;
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private Camera mainCamera;
    private string tag = "";
    private bool mission = false;
    private int cont = 0;
    private int contDone = 0;
    void Start()
    {
    }

    void Update()
    {
            ArtMission();
    }

    public void ItalianMission()
    {
        RelativeMovement.SetInMission(false);
        italianMission.gameObject.SetActive(true);
    }

    IEnumerator timer(){
        Debug.Log("Aspetto...");
        yield return new WaitForSecondsRealtime(5);
        mission = false;
        
    }

    public void ArtMission()
    {

        switch(tag)
        {
            case GameEvent.VITRUVIAN_TAG:
                
                break;
            case GameEvent.COLUMN_CORINTHIAN_TAG:
                // Movimento camera
                if (cont <= 0)
                {
                    mainCamera.gameObject.SetActive(false);
                    cameras[1].gameObject.SetActive(true);
                    RelativeMovement.SetInMission(true);
                    StartCoroutine(timer());
                    artMission.gameObject.SetActive(true);
                    ++cont;
                    ++contDone;
                }
                ArtSettings.SetText(artText.text.ToUpper());
                string tmp = artText.text.ToUpper();
                //Debug.Log(tmp);
                if(tmp == "COLONNA CORINZIA")
                {
                    Debug.Log("Corretto");
                }
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
