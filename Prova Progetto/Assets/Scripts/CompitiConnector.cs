using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


using UnityEngine.SceneManagement;


// Oggettino per mappare la risposta dal Server (Testato)

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


    // URL del Server (Si, l'abbiamo scritto veramente ma restituisce sempre lo stesso JSON, quindi tanto vale mockare tutto)
    private static string url = "http://localhost:8080/dyh";
    private string result = "";

    private Compiti compiti;
    private bool isServerOk = false;
    private bool areCompitiOk = false;


    // Elementi UI
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

    [SerializeField] TMP_Text fineText;

    [SerializeField] TMP_Text next;
    [SerializeField] GameObject enterIcon;


    // FINE ELEMENTI UI


    // Mi serve la reference al personaggio per cambiargli il punteggio
    [SerializeField] BambinoControllerAngiolettoMode bimbo;


    
    // Iteratore domande
    int lastDomandaRendered = 0;


    // Accumulatore risposte corrette
    private int risposteCorrette = 0;

    // Mi serve solo per attivare tutta la UI quando l'evento viene catturato
    public static bool siamoInModalitaCompiti = false;

    IEnumerator GetRequest(string url)
    {
        // Chiamo il server
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


                // Qui tecnicamente dovrei gestire gli errori del server o i not found
                // Ma dato che stiamo testando quasi sempre senza server
                // Mock dell'oggettino con 5 domande casuali
                // Per testare la UI dei compiti
                compiti = new Compiti();

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
        // Mando la chiamata come coroutine
        StartCoroutine(GetRequest(url));
        next.text = "";
        enterIcon.SetActive(false);
    }



    void Update()
    {

        if (siamoInModalitaCompiti)
        {
            // Lo faccio una sola volta
            if (isServerOk && !areCompitiOk)
            {
                // Setto attiva la pagina introduttiva
                areCompitiOk = true;
                inizio.SetActive(true);
                foglioCompiti.SetActive(false);
                fine.SetActive(false);
                riepilogo.SetActive(false);
                nienteCompiti.SetActive(false);
            }
            if (areCompitiOk)
            {
                next.text = "";
                enterIcon.SetActive(true);
                // Con "invio" cambio pagina
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

                    if (lastDomandaRendered < compiti.Domande.Length)
                    {
                        // Setto la prossima domanda in UI
                        domanda.text = compiti.Domande[lastDomandaRendered].TestoDomanda;
                        risposta1.text = compiti.Domande[lastDomandaRendered].Risposte[0];
                        risposta2.text = compiti.Domande[lastDomandaRendered].Risposte[1];
                        risposta3.text = compiti.Domande[lastDomandaRendered].Risposte[2];
                        risposta4.text = compiti.Domande[lastDomandaRendered].Risposte[3];



                    }
                    if (lastDomandaRendered > 0 && lastDomandaRendered <= compiti.Domande.Length)
                    {
                        // Controllo se quella precedente e' stata risposta correttamente
                        int rispostaCorretta = compiti.Domande[lastDomandaRendered - 1].RispostaCorretta;
                        if ((rispostaCorretta == 1 && risposta1Toggle.isOn) || (rispostaCorretta == 2 && risposta2Toggle.isOn) ||
                        (rispostaCorretta == 3 && risposta3Toggle.isOn) || (rispostaCorretta == 4 && risposta4Toggle.isOn))
                        {
                            this.risposteCorrette++;
                        }

                    }
                    // Ho finito le "domande", devo mostrare riepilogo se c'erano domande,
                    // O skippare alla pagina finale se non c'erano compiti per casa
                    if (lastDomandaRendered == compiti.Domande.Length)
                    {
                        // Caso c'erano compiti
                        if (compiti.Domande.Length > 0)
                        {
                            foglioCompiti.SetActive(false);
                            int angiolettoCoinstoUpdate = risposteCorrette * 10;
                            
                            // Aggiungo questo if perche pareva brutto fare i complimenti anche quando le sbagliavi tutte
                            if (risposteCorrette == 0) {
                                riepilogoText.text = "H";
                            }
                            else {
                                riepilogoText.text = "Complimenti, h";
                            }
                            // LA H viene aggiunta prima :)
                            riepilogoText.text = riepilogoText.text+"ai risposto correttamente a " + risposteCorrette + " domande su " + compiti.Domande.Length + "\n Quindi hai guadagnato " + angiolettoCoinstoUpdate + " angioletto coins";
                            
                            bimbo.UpdateAngioletto(angiolettoCoinstoUpdate);
                            riepilogo.SetActive(true);
                        }
                    }
                    // Questa e' la pagina di riepilogo
                    if (lastDomandaRendered == compiti.Domande.Length + 1)
                    {
                        riepilogo.SetActive(false);
                        fineText.text = "Hai totalizzato " + bimbo.getAngiolettoScore() + " angioletto coins\n" +
                        "Sei stata brava\n" +
                        "Hai fatto i tuoi compiti\n" +
                        "Ora corri da un adulto e chiedigli di scambiare\n" +
                        "i tuoi angioletto coins\n" +
                        "con un gelato.\n" +
                        "Non potranno dirti di NO\n" +
                        "(Io chiuderò un occhio sulle tue marachelle)";

                        fine.SetActive(true);

                    }
                    else if (fine.active)
                    {
                        // Passo ai credits, che poi mi riportano al menu principale
                        Time.timeScale = 1;
                        SceneManager.LoadScene("Credits");
                    }
                    lastDomandaRendered++;







                }
            }
            next.text = "";
            enterIcon.SetActive(false);
        }



    }

}