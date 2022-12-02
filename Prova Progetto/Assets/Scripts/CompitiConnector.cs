using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

[Serializable]
public class Domanda{
    public string TestoDomanda;
    public string[] Risposte;
    public string RispostaCorretta;
}

[Serializable]
public class Compiti{
    public Domanda[] Domande;

    public string toString() {
        Debug.Log(this);
        Debug.Log(this.Domande);
        foreach(Domanda d in this.Domande) {
            Debug.Log(d.TestoDomanda);
        }
        return Domande.ToString();
    }
}

public class CompitiConnector : MonoBehaviour
{
    private static string url = "http://localhost:8080/dyh";
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
                //Debug.Log(ricordamiDiCancellarla.json);
                Compiti c = JsonUtility.FromJson<Compiti>(result);

                Debug.Log(c.toString());
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
