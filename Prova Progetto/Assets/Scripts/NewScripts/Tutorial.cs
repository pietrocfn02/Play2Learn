using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    //[SerializeField] private TMP_Text labelText; // testo messaggi
    //[SerializeField] private GameObject imageText;  // immagine messaggi
    private bool _W = false;
    private bool _A = false;
    private bool _S = false;
    private bool _D = false;
    private bool _SPACE = false;
    void Start()
    {
       
    }

    IEnumerator TutorialMovement()
    {
        //labelText.text = "Benvenuto nel tutorial...";
        //imageText.SetActive(true);
        Debug.Log("Benvenuto nel tutorial...");
        yield return new WaitForSecondsRealtime(2);
        //labelText.text = "Iniziamo a vedere come muoversi.";
        Debug.Log("Iniziamo a vedere come muoversi.");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Per prima cosa usa i comandi ( W,A,S,D )per muoverti.");
       
        //labelText.text = "Bene, bravo! ";
        Debug.Log("Bene, bravo! ");
        yield return new WaitForSecondsRealtime(2);
        //labelText.text = "Ora Prova a fare un salto";
        Debug.Log("Ora Prova a fare un salto");
        yield return new WaitForSecondsRealtime(2);
        while(!_SPACE){
            //labelText.text = "Premi [SPACE] per saltare...";
            Debug.Log("Premi [SPACE] per saltare...");
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("Bounce");
                _SPACE = true;
            }
        }
        //labelText.text = "Complimenti, adesso sai come muoverti!!";
        Debug.Log("Complimenti, adesso sai come muoverti!!");


    }
// Update is called once per frame
    void Update()
    {
        
        //StartTutorial();
        
    }
    public void StartTutorial()
    {
        StartCoroutine(TutorialMovement());
    }
}
