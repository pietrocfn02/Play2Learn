using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[Serializable]
public class Domanda
{
    public string TestoDomanda;
    public string[] Risposte;
    public int RispostaCorretta;
}

[Serializable]
public class Compiti
{
    public Domanda[] Domande;

}

public class CompitiConnector : MonoBehaviour
{
    private static string url = "http://localhost:8080/dyh";
    private string result = "";
    private Compiti compiti;
    private bool isServerOk = false;
    private bool areCompitiOk = false;
    [SerializeField] TMP_Text domanda;
    [SerializeField] Toggle risposta1Toggle;
    [SerializeField] Toggle risposta2Toggle;
    [SerializeField] Toggle risposta3Toggle;
    [SerializeField] Toggle risposta4Toggle;

    [SerializeField] TMP_Text risposta1;
    [SerializeField] TMP_Text risposta2;
    [SerializeField] TMP_Text risposta3;
    [SerializeField] TMP_Text risposta4;


    [SerializeField] GameObject nienteCompiti;

    [SerializeField] GameObject foglioCompiti;
    [SerializeField] GameObject inizio;
    [SerializeField] GameObject riepilogo;
    [SerializeField] GameObject fine;

    [SerializeField] TMP_Text riepilogoText;


    int lastDomandaRendered = 0;

    private int risposteCorrette = 0;

    public static bool siamoInModalitaCompiti = false;

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest x = UnityWebRequest.Get(url))
        {
            yield return x.SendWebRequest();
            isServerOk = true;
            if (x.result == UnityWebRequest.Result.Success)
            {
                result = x.downloadHandler.text;
                compiti = JsonUtility.FromJson<Compiti>(result);


            }
            else
            {
                // Debug.Log("Non hai compiti per lunedì");

                compiti = new Compiti();

                // TODO: MOCK DA ELIMINARE
                compiti.Domande = new Domanda[5];
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
    }



    void Update()
    {

        if (siamoInModalitaCompiti)
        {
            // Lo faccio una sola volta
            if (isServerOk && !areCompitiOk)
            {
                areCompitiOk = true;
                inizio.SetActive(true);
                foglioCompiti.SetActive(false);
                fine.SetActive(false);
                riepilogo.SetActive(false);
                nienteCompiti.SetActive(false);
            }
            if (areCompitiOk)
            {
                if (Input.GetKeyUp(KeyCode.Return))
                {

                    if (lastDomandaRendered == 0)
                    {
                        inizio.SetActive(false);
                        if (compiti.Domande.Length > 0)
                        {
                            foglioCompiti.SetActive(true);
                        }
                        else
                        {
                            fine.SetActive(true);
                        }
                    }

                    
                        if (lastDomandaRendered > 0 && lastDomandaRendered <= compiti.Domande.Length)
                        {
                            //Check sulle risposte Giuste;
                            //Per il momento le do tutte giuste poi e' da capire
                            this.risposteCorrette++;
                        }

                        if (lastDomandaRendered < compiti.Domande.Length)
                        {
                            Debug.Log("PREMO Invio "+lastDomandaRendered );
                            Debug.Log(compiti.Domande);
                            Debug.Log(compiti.Domande[lastDomandaRendered]);
                            Debug.Log(compiti.Domande[lastDomandaRendered].TestoDomanda);
                            domanda.text = compiti.Domande[lastDomandaRendered].TestoDomanda;
                            risposta1.text = compiti.Domande[lastDomandaRendered].Risposte[0];
                            risposta2.text = compiti.Domande[lastDomandaRendered].Risposte[1];
                            risposta3.text = compiti.Domande[lastDomandaRendered].Risposte[2];
                            risposta4.text = compiti.Domande[lastDomandaRendered].Risposte[3];

                        }
                        if (lastDomandaRendered == compiti.Domande.Length)
                        {
                            if (compiti.Domande.Length >0) {
                                foglioCompiti.SetActive(false);
                                riepilogo.SetActive(true);
                            }
                            else lastDomandaRendered++;
                            

                        }
                        if (lastDomandaRendered == compiti.Domande.Length + 1)
                        {
                            riepilogo.SetActive(false);
                            fine.SetActive(true);
                        }
                        else
                        {
                            //Carica Scena "Credits"
                        }
                        lastDomandaRendered++;
                    


                    



                }
            }
        }



    }

}
