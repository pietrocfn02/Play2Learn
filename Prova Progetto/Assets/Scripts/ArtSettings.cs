using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtSettings : MonoBehaviour
{
    private static TMP_Text tmpText;
    private static int cont = 0; 
    void Start()
    {
        tmpText = this.GetComponent<TMP_Text>();
    }
    public static void SetText(string textEdit)
    {
        if(cont < 1)
            Debug.Log(tmpText.name);
        tmpText.text = textEdit;
        cont++;
    }

}
