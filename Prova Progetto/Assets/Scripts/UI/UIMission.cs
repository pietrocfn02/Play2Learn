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
    [SerializeField] private TMP_Text questionTest;
    private string tag = "";
    private bool mission = false;
    private int cont = 0;
    private int contDone = 0;
    void Start()
    {
        questionTest.text = "";
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
    private void SetCamera(string tag, int camera)
    {
        if (cont <= 0)
        {
            mainCamera.gameObject.SetActive(false);
            cameras[camera].gameObject.SetActive(true);
            RelativeMovement.SetInMission(true);
            StartCoroutine(timer());
            artMission.gameObject.SetActive(true);
            ++cont;
            ++contDone; // contatore per le missioni complete
            questionTest.text = Questions.GetRandomQuestion(tag);
        }
        ArtSettings.SetText(artText.text.ToUpper());
    }
    private void SpawnMiniature(int prefab)
    {
        Vector3[] positions = { new Vector3(22.4f,1.81f,12.37f), 
                                new Vector3(22.945f,1.81f,14.93f),
                                new Vector3(25.32f,1.74f,13.3f), 
                                new Vector3(27.4f,1.94f,13.28f), 
                                new Vector3(27.4f,1.922f,12.7f), 
                                new Vector3(27.4f,1.922f,12.25f), 
                                new Vector3(27.4f,1.922f,11.61f) 
                              };
        Instantiate(artPrefabs[0],positions[prefab], Quaternion.identity);
    }
    public void ArtMission()
    {

        switch(tag)
        {
            case GameEvent.VITRUVIAN_TAG:
                SetCamera(GameEvent.VITRUVIAN_TAG, 0);
                SpawnMiniature(3);
                break;
            case GameEvent.COLUMN_CORINTHIAN_TAG:
                // Movimento camera
                SetCamera(GameEvent.COLUMN_CORINTHIAN_TAG, 1);
                
                string tmp = artText.text.ToUpper();
                if(tmp == "COLONNA CORINZIA")
                {
                    Debug.Log("Corretto");
                }
                break;

            case GameEvent.COLUMN_IONIC_TAG:
                SetCamera(GameEvent.COLUMN_IONIC_TAG, 2);
                break;

            case GameEvent.TOPOLINO_TAG:
                SetCamera(GameEvent.TOPOLINO_TAG, 3);
                break;

            case GameEvent.ONEPIECE_TAG:
                SetCamera(GameEvent.ONEPIECE_TAG, 4);
                break;

            case GameEvent.SNOOPY_TAG:
                SetCamera(GameEvent.SNOOPY_TAG, 5);
                break;

            case GameEvent.SUPERMAN_TAG:
                SetCamera(GameEvent.SUPERMAN_TAG, 6);
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
