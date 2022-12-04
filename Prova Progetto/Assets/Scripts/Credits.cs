using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;

using TMPro;


public class Credits : MonoBehaviour
{
    // Start is called before the first frame update

    private static string[] phrases = {
        "AVETE GIOCATO A PLAY2LEARN",
        "IDEATO E SVILUPPATO DA P.E.M. GROUP",
        "PER IL CORSO DI VIRTUAL REALITY",
        "DE.MA.CS. - UNIVERSITA' DELLA CALABRIA",
        "ANNO ACCADEMICO 2022/2023",
        "...",
        "DIAVOLETTO COINS E ANGIOLETTO COINS",
        "NON SONO CRIPTOVALUTE",
        "...",
        "MA QUI POTRAI TROVARE IL TASSO DI CAMBIO:",
        "20 ANGIOLETTO COINS VALGONO UN GELATO",
        "25 DIAVOLETTO COINS UN PIATTO DI VERDURE A CENA",
        "...",
        "RICORDATE, BAMBINI. SE FATE I COMPITI IL SABATO",
        "NON DOVETE FARLI LA DOMENICA",
        "E NON FATE ARRABBIARE IL FANTASMINO CARMELO",
        "FINE",
        "FINE",
        "FINE",
        "FINE"};



    [SerializeField] TMP_Text customizableText;


    private float waitTime = 3.00f;
    private float currentWaitTime = 0f;

    private int index = 0;


    void Start()
    {
        customizableText.text = "";

    }


    void Update()
    {

        currentWaitTime = currentWaitTime - Time.deltaTime;
        if (currentWaitTime <= 0 && index <= phrases.Length)
        {
            currentWaitTime = waitTime;
            if (index < phrases.Length)
            {
                customizableText.text = phrases[index];
                index++;
            }
            else
            {
                SceneManager.LoadScene("StartGame_Scene");
            }
        }





    }

}
