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
    [SerializeField] private TMP_Text[] missionText;
    [SerializeField] private CinemachineFreeLook mainCamera;
    [SerializeField] private TMP_Text questionTest;
    [SerializeField] private TMP_Text[] panelText;

    private bool isActive = false;
    private string tag = "";
    private bool mission = false;
    private int contDone = 0;
    private bool spawned = false;
    private string artTmp = "";
    private GameObject newArtPrefab;

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
        // RelativeMovement.StopMovement(true);
        italianMission.gameObject.SetActive(true);
    }
    public void EndItalian()
    {
        // RelativeMovement.StopMovement(false);
        italianMission.gameObject.SetActive(false);
        Messenger.Broadcast("MissionComplete");
    }
    IEnumerator timer(){
        yield return new WaitForSecondsRealtime(5);
        mission = false;
        
    }
    private void ActiveArtCameras(string tag, int camera)
    {
        if (!isActive) // && !mission[i] e la missione i non è completa
        {
            mainCamera.gameObject.SetActive(false);
            cameras[camera].gameObject.SetActive(true);
            // RelativeMovement.StopMovement(true);
            artMission.gameObject.SetActive(true);
            isActive = true;
            questionTest.text = Questions.GetRandomQuestion(tag);
            spawned = false;
        }
    }

    private void DeactivateArtCameras(int camera)
    {
        if (isActive)
        {
            artMission.gameObject.SetActive(false);
            cameras[camera].gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            // RelativeMovement.StopMovement(false);
            isActive = false;
        }
    }
    private void SpawnMiniature(int prefab)
    {
        if (!spawned){
            Vector3[] positions = { new Vector3(22.49f,1.75f,12.4f), 
                                    new Vector3(22.88f,1.8f,14.985f),
                                    new Vector3(25.3f,1.7f,13.38f), 
                                    new Vector3(27.4f,1.73f,13.28f), 
                                    new Vector3(27.4f,1.73f,12.7f), 
                                    new Vector3(27.4f,1.73f,12.25f), 
                                    new Vector3(27.4f,1.73f,11.61f) 
                                };
            
            newArtPrefab = Instantiate(artPrefabs[prefab],positions[prefab], Quaternion.identity);
            if (prefab >= 3)
                newArtPrefab.transform.eulerAngles = new Vector3(90f, -90f, 0f);
            else
                newArtPrefab.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            newArtPrefab.name = newArtPrefab.name.Replace("(Clone)","");
            spawned = true;
        }
    }

    // Prendere le risposte corrette dal database --> 
    private string[] correctAnswers = {     "UOMO VITRUVIANO",
                                            "COLONNA CORINZIA",
                                            "COLONNA IONICA",
                                            "TOPOLINO",
                                            "ONE PIECE",
                                            "SNOOPY",
                                            "SUPER MAN"
                                        };

    // <--
    public void SetMission()
    {
        artTmp = artText.text.ToUpper();
    }

    private void DoMission(string tag, int i)
    {
        if (correctAnswers[i] != "")
        {
            RelativeMovement.StopMovement(true);
            ActiveArtCameras(tag, i);
            SpawnMiniature(i);
            missionText[i].gameObject.SetActive(true);
            panelText[i].text = artText.text.ToUpper();
            panelText[i].transform.parent.parent.GetComponent<BoxCollider>().isTrigger = false;
            panelText[i].transform.parent.parent.GetComponent<BoxCollider>().size = new Vector3(0.02f,-0.07f,-0.005f);
            panelText[i].transform.parent.parent.GetComponent<BoxCollider>().center = new Vector3(0.218f,0.03f,0f);
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (artTmp.Contains(correctAnswers[i]) && artTmp.Length-1 == correctAnswers[i].Length)
                {
                    DeactivateArtCameras(i);
                    correctAnswers[i] = "";
                    if (i >= 3)
                    {
                        newArtPrefab.transform.eulerAngles = new Vector3(90f, -90f, 0f);
                        newArtPrefab.GetComponent<BoxCollider>().isTrigger = true;  
                        newArtPrefab.GetComponent<BoxCollider>().size = new Vector3(0.4f,0.35f,0.7f);
                        newArtPrefab.GetComponent<BoxCollider>().center = new Vector3(0f,0.12f,0f);
                    }
                    else
                        newArtPrefab.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                    newArtPrefab.GetComponent<BoxCollider>().isTrigger = true;
                    Destroy(newArtPrefab.GetComponent<RotateWithMouse>());
                    ++contDone;
                    RelativeMovement.StopMovement(false);
                }
            }
        }
    }
    public void ArtMission()
    {
        switch(tag)
        {
            case GameEvent.SIGN1_TAG:
                DoMission(GameEvent.SIGN1_TAG, 0);
                break;
            case GameEvent.SIGN2_TAG:
                DoMission(GameEvent.SIGN2_TAG, 1);
                break;
            case GameEvent.SIGN3_TAG:
                DoMission(GameEvent.SIGN3_TAG, 2);
                break;
            case GameEvent.SIGN4_TAG:
                DoMission(GameEvent.SIGN4_TAG, 3);
                break;
            case GameEvent.SIGN5_TAG:
                DoMission(GameEvent.SIGN5_TAG, 4);
                break;
            case GameEvent.SIGN6_TAG:
                DoMission(GameEvent.SIGN6_TAG, 5);
                break;
            case GameEvent.SIGN7_TAG:
                DoMission(GameEvent.SIGN7_TAG, 6);
                break;
            default:
                break;    
        }
        if(contDone >= 7)
        {
            Messenger.Broadcast("MissionArtDone");
        }
    }
  

    private void SetSign1()
    {
        tag = GameEvent.SIGN1_TAG;
    }
    private void SetSign2()
    {
        tag = GameEvent.SIGN2_TAG;
    }
    private void SetSign3()
    {
        tag = GameEvent.SIGN3_TAG;
    }
    private void SetSign4()
    {
        tag = GameEvent.SIGN4_TAG;
    }
    private void SetSign5()
    {
        tag = GameEvent.SIGN5_TAG;
    }
    private void SetSign6()
    {
        tag = GameEvent.SIGN6_TAG;
    }
    private void SetSign7()
    {
        tag = GameEvent.SIGN7_TAG;
    }

    private void UIMathMission()
    {
        mainCamera.gameObject.SetActive(false);
        cameras[7].gameObject.SetActive(true);
    }

    private void MathCamDone()
    {
        cameras[7].gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.FIRST_UI_MISSION, ItalianMission);
        Messenger.AddListener("EndItalian", EndItalian);
        Messenger.AddListener(GameEvent.SIGN1_TAG, SetSign1);
        Messenger.AddListener(GameEvent.SIGN2_TAG, SetSign2);
        Messenger.AddListener(GameEvent.SIGN3_TAG, SetSign3);
        Messenger.AddListener(GameEvent.SIGN4_TAG, SetSign4);
        Messenger.AddListener(GameEvent.SIGN5_TAG, SetSign5);
        Messenger.AddListener(GameEvent.SIGN6_TAG, SetSign6);
        Messenger.AddListener(GameEvent.SIGN7_TAG, SetSign7);
        Messenger.AddListener("UIMathMission", UIMathMission);
        Messenger.AddListener("MathCamDone", MathCamDone);
    }

    void onDestroy()
    {
        Messenger.RemoveListener(GameEvent.FIRST_UI_MISSION, ItalianMission);
        Messenger.RemoveListener("EndItalian", EndItalian);
        Messenger.RemoveListener(GameEvent.SIGN1_TAG, SetSign1);
        Messenger.RemoveListener(GameEvent.SIGN2_TAG, SetSign2);
        Messenger.RemoveListener(GameEvent.SIGN3_TAG, SetSign3);
        Messenger.RemoveListener(GameEvent.SIGN4_TAG, SetSign4);
        Messenger.RemoveListener(GameEvent.SIGN5_TAG, SetSign5);
        Messenger.RemoveListener(GameEvent.SIGN6_TAG, SetSign6);
        Messenger.RemoveListener(GameEvent.SIGN7_TAG, SetSign7);
        Messenger.RemoveListener("UIMathMission", UIMathMission);
        Messenger.RemoveListener("MathCamDone", MathCamDone);
    }
}
