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

}

public class CompitiConnector : MonoBehaviour
{
    private static string url = "http://localhost:8080/dyh";
    private string result = "";
    private Compiti compiti;
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
                compiti = JsonUtility.FromJson<Compiti>(result);

                
            }
            else
            {
                Debug.Log("Non hai compiti per lunedì");
                
                compiti = new Compiti();
                
                // TODO: MOCK DA ELIMINARE
                compiti.Domande = new Domande[5];
                Domanda d1 = new Domanda();
                d1.TestoDomanda = "Quale regione si trova più a Nord?";
                d1.Risposte = new string[4];
                d1.Risposte[0] = "Calabria";
                d1.Risposte[1] = "Molise";
                d1.Risposte[2] = "Sardegna";
                d1.Risposte[3] = "Lombardia";
                d1.RispostaCorretta = 4;
                compiti.Domande[0] = d1;
                
                Domanda d2 = new Domanda();
                d2.TestoDomanda = "Chi prende il voto più alto all'esame?";
                d2.Risposte = new string[4];
                d2.Risposte[0] = "Pietro";
                d2.Risposte[1] = "Elio";
                d2.Risposte[2] = "Martina";
                d2.Risposte[3] = "il fantasmino Carmelo";
                d2.RispostaCorretta = 4;
                compiti.Domande[1] = d2;

                Domanda d3 = new Domanda();
                d3.TestoDomanda = "Chi ha scritto questo JSON?";
                d3.Risposte = new string[4];
                d3.Risposte[0] = "Pietro";
                d3.Risposte[1] = "Elio";
                d3.Risposte[2] = "Martina";
                d3.Risposte[3] = "il fantasmino Carmelo";
                d3.RispostaCorretta = 1;
                compiti.Domande[2] = d3;

                Domanda d4 = new Domanda();
                d4.TestoDomanda = "In che anno è stata scoperta l'America?";
                d4.Risposte = new string[4];
                d4.Risposte[0] = "1922";
                d4.Risposte[1] = "1789";
                d4.Risposte[2] = "1492";
                d4.Risposte[3] = "il fantasmino Carmelo";
                d4.RispostaCorretta = 3;
                compiti.Domande[3] = d4;


                Domanda d5 = new Domanda();
                d5.TestoDomanda = "Quanto fa 7 x 7?";
                d5.Risposte = new string[4];
                d5.Risposte[0] = "77";
                d5.Risposte[1] = "49";
                d5.Risposte[2] = "17";
                d5.Risposte[3] = "il fantasmino Carmelo";
                d5.RispostaCorretta = 2;
                compiti.Domande[4] = d5;

            }
        }
    }

    void Start()
    {
        StartCoroutine(GetRequest(url));
        //Debug.Log(result);
    }



    void Update() {

    }

}
