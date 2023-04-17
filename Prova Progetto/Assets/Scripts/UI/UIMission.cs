using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMission : MonoBehaviour
{
    [SerializeField] private GameObject sentenceCanvas; // L' empty che contiene la frase da comporre
    [SerializeField] private GameObject wordCanvas;     // L' empty che contiene le parole con cui comporre la frase
    [SerializeField] private GameObject[] words;        // Le parole collezzionate nella misisone 1 del tutorial
    [SerializeField] private GameObject[] facede;       // La posizione corretta delle parole public GameObject selectedObject;
    private Vector3 pos;
    private float speed = 10f;
    private Camera cam;

    void Start()
    {
        
        words = new GameObject[10];
        facede = new GameObject[10];
        
    }

    void Update()
    {
      
    }

    void OnMouseDown()
    {
        pos = transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + pos, speed * Time.deltaTime);
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
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
        cam = Camera.main;
        Messenger.AddListener(GameEvent.FIRST_UI_MISSION, RearrangeSentence);
    }
}
