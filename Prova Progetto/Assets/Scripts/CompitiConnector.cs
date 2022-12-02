using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
/*
public static class ricordamiDiCancellarla{
    public static string json = "{\"Domande\":[{\"TestoDomanda\":\"Chièpiùbello?\",\"Risposte\":[\"Pietro\",\"Elio\",\"Martina\",\"ilfantasminoCarmelo\"],\"RispostaCorretta\":4},{\"TestoDomanda\":\"Chiprendeilvotopiùaltoall'esame?\",\"Risposte\":[\"Pietro\",\"Elio\",\"Martina\",\"ilfantasminoCarmelo\"],\"RispostaCorretta\":4},{\"TestoDomanda\":\"ChihascrittoquestoJSON?\",\"Risposte\":[\"Pietro\",\"Elio\",\"Martina\",\"ilfantasminoCarmelo\"],\"RispostaCorretta\":1},{\"TestoDomanda\":\"Incheannoèstatascopertal'America\",\"Risposte\":[\"1922\",\"1789\",\"1492\",\"ilfantasminoCarmelo\"],\"RispostaCorretta\":3},{\"TestoDomanda\":\"Quantofa7x7\",\"Risposte\":[\"77\",\"49\",\"17\",\"ilfantasminoCarmelo\"],\"RispostaCorretta\":2}]}";

}

[Serializable]
public class Domanda{
    public string TestoDomanda{get; set;}    
    public string[] Risposte{get; set;}
    public string RispostaCorretta{get; set;}
}

[Serializable]
public class Compiti{
    public Domanda[] Domande{get; set;}
}
*/
public class CompitiConnector : MonoBehaviour
{
    private static string url = "https://dummyjson.com/products/7";
    private string result = "";
    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest x = UnityWebRequest.Get(url))
        {
            yield return x.SendWebRequest();
            if (x.result == UnityWebRequest.Result.Success)
            {
                result = x.downloadHandler.text;
                Debug.Log("RES: "+result );
                Debug.Log("RES x: "+x);
                //result = ricordamiDiCancellarla.json;
                //Compiti c = Compiti.CreateFromJSON(result);
                //Debug.Log(c);
            }
            else
            {
                result = "Non hai compiti per lunedì";
            }
        }
    }

    void Start()
    {
        StartCoroutine(GetRequest(url));
        Debug.Log(result);
    }

}
