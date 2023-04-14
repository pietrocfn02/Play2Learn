using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMission : MonoBehaviour
{
    [SerializeField] private GameObject sentenceCanvas; // L' empty che contiene la frase da comporre
    [SerializeField] private GameObject wordCanvas;     // L' empty che contiene le parole con cui comporre la frase
    [SerializeField] private GameObject[] words;        // Le parole collezzionate nella misisone 1 del tutorial
    private Vector3 mousePosition;                      // La posizione del mouse per il drag and drop
    
    
    
    void Start()
    {
        words = new GameObject[10];   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RearrangeSentence()
    {
        
    }

    void onDestroy()
    {
        Messenger.RemoveListener(GameEvent.FIRST_UI_MISSION, RearrangeSentence);
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.FIRST_UI_MISSION, RearrangeSentence);
    }
}
