using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMission : MonoBehaviour
{
    [SerializeField] private Canvas firstMission;

    void Start()
    {
    }

    void Update()
    {
      
    }

    public void FirstMission()
    {
        Time.timeScale = 0;
        firstMission.gameObject.SetActive(true);
    }

    void onDestroy()
    {
        Messenger.RemoveListener(GameEvent.FIRST_UI_MISSION, FirstMission);
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.FIRST_UI_MISSION, FirstMission);
    }
}
