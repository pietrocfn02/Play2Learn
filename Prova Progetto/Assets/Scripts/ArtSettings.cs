using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtSettings : MonoBehaviour
{
    private static TMP_Text tmpText;
    
    void Start()
    {
        tmpText = this.GetComponent<TMP_Text>();
    }
    public static void SetText(string textEdit)
    {
        tmpText.text = textEdit;
    }

}
