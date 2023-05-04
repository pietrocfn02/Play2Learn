using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Questions
{
    private static string[] vitruvianQuestion = {   "Qual'è il disegno che rappresenta l'unione simbolica tra arte e scienza?",
                                                    "In questo disegno sono preseti due strutture geometriche: -il quadrato che rappresenta la Terra; - il cerchio che rappresenta l'Universo. Mi sapresti dire i lsuo nome?",
                                                    "Questo è uno dei disegni più famosi di Leonardo da Vinci. Esso indica una sintesi sugli studi del corpo umano. Ricordi come si chiama?",
                                                    "Quale famoso disegno rappresenta un uomo con due braccia e due gambe racchiuso in un quadrato e un cerchio?"
                                                };

    private static string[] corinthianQuestion = {  "Quale tipo di colonna è caratterizzata da un capitello con decorazioni a volute e foglie di acanto?",
                                                    "La base attica semplificata di questa colonna è composta da plinto, toro, scozia, toro. Mi sapresti dire il suo nome?",
                                                    "Quale tipo di colonna, usata alla fine del V secolo a.C., ha un capitello composto da un giro semplice o doppio di foglie d'acanto?",
                                                    "Mi sapresti dire il nome della colonna il cui fusto (difronte a te) che presenta venti scanalature a spigolo vivo?"
                                                };
    private static string[] ionicQuestion = {       "Mi sapresti indicare il dipo di colonna che non poggia sullo stilobate, ma direttamente su una base formata da due elementi ovvero il toro, di forma convessa e la scozia, di forma concava?",
                                                    "L'ordine di cui fa parte questo tipo di colonna rielabora motivi orientali. Qual'è il nome della colonna appartenete al secondo dei tre ordini classici?",
                                                    "Qual'è il nome della colonna che presenta come fusto un tipo più snello e con 24 scalanature separate da distelli?",
                                                    "Questa colonna ha il problema di mostrarse le due facce principali con delle volute, mentre sui lati ha un pulvino. Puoi scrivere il nome di questo tipo di colonna? "
                                            };
    private static string[] topolinoQuestion = {    "Il fumetto che vedi difronte a te è conosciuto in molti paesi, non che negli StatiUniti, il suo paese di origine, come -Mickey Mouse-. Con che nome è conosciuto in Italia?",
                                                    "Come si chiama il fumetto il cui personaggio ha delle orecchie definite come << una delle più grandi icone del 20simo e del 21esimo secolo >>?",
                                                    "Qual'è il fumetto il cui personaggio venno creato da Walt Disney e Ub Iwerks nel gennaio del 1928?",
                                                    "Come si chiama il fumetto, difronte a te, che ha come protagonnista un topo che vanta di aver posato con quasi tutti i presidenti degli Stati Uniti?"
                                                };
    private static string[] one_pieceQuestion = {   "Qual'è il nome del fumetto disegnato da Eiichiro Oda, iniziato nel luglio del 1997 e che continua da oltre 25 anni?",
                                                    "Il fumetto che hai davanti, con oltre 516 milioni di copie, è il fumetto, del suo genere, ad aver venduto di più al mondo. Sai dirmi come si chiama?",
                                                    "Questo fumetto è stato finalista di molti concorsi, tra cui il premio Osamu Tezuka che va dal 2000 al 2002 (tre anni consecutivi). Sai scrivermi il suo nome?",
                                                    "Come si chiama il fumetto la cui prima sigla di apertura della serie animata, We Are!, è stata premiata come miglior sigla nel 2000?"
                                                };
    private static string[] snoopyQuestion = {      "Curiosità_Snoopy_1",
                                                    "Curiosità_Snoopy_2",
                                                    "Curiosità_Snoopy_3",
                                                    "Curiosità_Snoopy_4"
                                                };
    private static string[] super_manQuestion = {   "Curiosità_Superman_1",
                                                    "Curiosità_Superman_2",
                                                    "Curiosità_Superman_3",
                                                    "Curiosità_Superman_4"
                                                };

    // Inserendo il tag, restituisce la domanda relativa a quest'ultimo
    public static string GetRandomQuestion(string questionType)
    {
        string returnQuestion = "";
        int random = 0;
        switch (questionType) 
        {
            case GameEvent.VITRUVIAN_TAG:
                random = Random.Range(0, 4);
                returnQuestion = vitruvianQuestion[random];
                break;
            case GameEvent.COLUMN_CORINTHIAN_TAG:
                random = Random.Range(0, 4);
                returnQuestion = corinthianQuestion[random];
                break;
            case GameEvent.COLUMN_IONIC_TAG:
                random = Random.Range(0, 4);
                returnQuestion = ionicQuestion[random];
                break;
            case GameEvent.TOPOLINO_TAG:
                random = Random.Range(0, 4);
                returnQuestion = topolinoQuestion[random];
                break;
            case GameEvent.ONEPIECE_TAG:
                random = Random.Range(0, 4);
                returnQuestion = one_pieceQuestion[random];
                break;
            case GameEvent.SNOOPY_TAG:
                random = Random.Range(0, 4);
                returnQuestion = snoopyQuestion[random];
                break;
            case GameEvent.SUPERMAN_TAG:
                random = Random.Range(0, 4);
                returnQuestion = super_manQuestion[random];
                break;
            
            default: 
                break;

        }
        return returnQuestion;

    }
}
