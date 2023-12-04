using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour
{

    [SerializeField] private GameObject settingsPanel;
    private Animator _animator;
    // True se il menu è aperto, false altrimenti 
    private bool _switch = false;
    // Aggiungere sound controller per i suoni del menu

    void Start()
    {
        _animator = settingsPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !_switch){
           settingsPanel.SetActive(true);
           _switch = true;
           _animator.SetBool("Open" ,true);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && _switch)
        {
            settingsPanel.SetActive(false);
            _switch = false;
           _animator.SetBool("Open" ,false);
        }
    }
}
