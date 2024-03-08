using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Questions
{
    private static string[] sign1Question = {       "Qual'� il disegno che rappresenta l'unione simbolica tra arte e scienza?",
                                                    "In questo disegno sono preseti due strutture geometriche: -il quadrato che rappresenta la Terra; - il cerchio che rappresenta l'Universo. Mi sapresti dire i lsuo nome?",
                                                    "Questo � uno dei disegni pi� famosi di Leonardo da Vinci. Esso indica una sintesi sugli studi del corpo umano. Ricordi come si chiama?",
                                                    "Quale famoso disegno rappresenta un uomo con due braccia e due gambe racchiuso in un quadrato e un cerchio?"
                                                };

    private static string[] sign2Question = {  "Quale tipo di colonna � caratterizzata da un capitello con decorazioni a volute e foglie di acanto?",
                                                    "La base attica semplificata di questa colonna � composta da plinto, toro, scozia, toro. Mi sapresti dire il suo nome?",
                                                    "Quale tipo di colonna, usata alla fine del V secolo a.C., ha un capitello composto da un giro semplice o doppio di foglie d'acanto?",
                                                    "Mi sapresti dire il nome della colonna il cui fusto (difronte a te) che presenta venti scanalature a spigolo vivo?"
                                                };
    private static string[] sign3Question = {       "Mi sapresti indicare il dipo di colonna che non poggia sullo stilobate, ma direttamente su una base formata da due elementi ovvero il toro, di forma convessa e la scozia, di forma concava?",
                                                    "L'ordine di cui fa parte questo tipo di colonna rielabora motivi orientali. Qual'� il nome della colonna appartenete al secondo dei tre ordini classici?",
                                                    "Qual'� il nome della colonna che presenta come fusto un tipo pi� snello e con 24 scalanature separate da distelli?",
                                                    "Questa colonna ha il problema di mostrarse le due facce principali con delle volute, mentre sui lati ha un pulvino. Puoi scrivere il nome di questo tipo di colonna? "
                                            };
    private static string[] sign4Question = {    "Il fumetto che vedi difronte a te � conosciuto in molti paesi, non che negli StatiUniti, il suo paese di origine, come -Mickey Mouse-. Con che nome � conosciuto in Italia?",
                                                    "Come si chiama il fumetto il cui personaggio ha delle orecchie definite come << una delle pi� grandi icone del 20simo e del 21esimo secolo >>?",
                                                    "Qual'� il fumetto il cui personaggio venno creato da Walt Disney e Ub Iwerks nel gennaio del 1928?",
                                                    "Come si chiama il fumetto, difronte a te, che ha come protagonnista un topo che vanta di aver posato con quasi tutti i presidenti degli Stati Uniti?"
                                                };
    private static string[] sign5Question = {   "Qual'� il nome del fumetto disegnato da Eiichiro Oda, iniziato nel luglio del 1997 e che continua da oltre 25 anni?",
                                                    "Il fumetto che hai davanti, con oltre 516 milioni di copie, � il fumetto, del suo genere, ad aver venduto di pi� al mondo. Sai dirmi come si chiama?",
                                                    "Questo fumetto � stato finalista di molti concorsi, tra cui il premio Osamu Tezuka che va dal 2000 al 2002 (tre anni consecutivi). Sai scrivermi il suo nome?",
                                                    "Come si chiama il fumetto la cui prima sigla di apertura della serie animata, We Are!, � stata premiata come miglior sigla nel 2000?"
                                                };
    private static string[] sign6Question = {      "Curiosit�_Snoopy_1",
                                                    "Curiosit�_Snoopy_2",
                                                    "Curiosit�_Snoopy_3",
                                                    "Curiosit�_Snoopy_4"
                                                };
    private static string[] sign7Question = {       "Curiosit�_Superman_1",
                                                    "Curiosit�_Superman_2",
                                                    "Curiosit�_Superman_3",
                                                    "Curiosit�_Superman_4"
                                                };

    // Inserendo il tag, restituisce la domanda relativa a quest'ultimo
    public static string GetRandomQuestion(string questionType)
    {
        string returnQuestion = "";
        int random = 0;
        switch (questionType) 
        {
            case GameEvent.SIGN1_TAG:
                random = Random.Range(0, 4);
                returnQuestion = sign1Question[random];
                break;
            case GameEvent.SIGN2_TAG:
                random = Random.Range(0, 4);
                returnQuestion = sign2Question[random];
                break;
            case GameEvent.SIGN3_TAG:
                random = Random.Range(0, 4);
                returnQuestion = sign3Question[random];
                break;
            case GameEvent.SIGN4_TAG:
                random = Random.Range(0, 4);
                returnQuestion = sign4Question[random];
                break;
            case GameEvent.SIGN5_TAG:
                random = Random.Range(0, 4);
                returnQuestion = sign5Question[random];
                break;
            case GameEvent.SIGN6_TAG:
                random = Random.Range(0, 4);
                returnQuestion = sign6Question[random];
                break;
            case GameEvent.SIGN7_TAG:
                random = Random.Range(0, 4);
                returnQuestion = sign7Question[random];
                break;
            
            default: 
                break;

        }
        return returnQuestion;

    }
}
