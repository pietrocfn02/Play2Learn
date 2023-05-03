using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtSettings : MonoBehaviour
{
    private static TMP_Text panelText;
    private static string toModify;
    void Start()
    {
        panelText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        panelText.text = toModify;
        if (panelText.text == "TOPOLINO")
        {
            Debug.Log("CORRETTO");
        }
    }

}
