package com.example.play2learnexercises.api;


import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class ExercisesApi {

        private static final String response = "{\n" +
                "    \"Domande\" :[\n" +
                "        {\n" +
                "            \"TestoDomanda\": \"Quale regione si trova più a Nord?\",\n" +
                "            \"Risposte\": [\n" +
                "                \"Calabria\",\n" +
                "                \"Molise\",\n" +
                "                \"Sardegna\",\n" +
                "                \"Lombardia\"\n" +
                "            ],\n" +
                "            \"RispostaCorretta\": 4\n" +
                "        },\n" +
                "        {\n" +
                "            \"TestoDomanda\": \"Chi prende il voto più alto all'esame?\",\n" +
                "            \"Risposte\": [\n" +
                "                \"Pietro\",\n" +
                "                \"Elio\",\n" +
                "                \"Martina\",\n" +
                "                \"il fantasmino Carmelo\"\n" +
                "            ],\n" +
                "            \"RispostaCorretta\": 4\n" +
                "        },\n" +
                "        {\n" +
                "            \"TestoDomanda\": \"Chi ha scritto questo JSON?\",\n" +
                "            \"Risposte\": [\n" +
                "                \"Pietro\",\n" +
                "                \"Elio\",\n" +
                "                \"Martina\",\n" +
                "                \"il fantasmino Carmelo\"\n" +
                "            ],\n" +
                "            \"RispostaCorretta\": 1\n" +
                "        },\n" +
                "        {\n" +
                "            \"TestoDomanda\": \"In che anno è stata scoperta l'America\",\n" +
                "            \"Risposte\": [\n" +
                "                \"1922\",\n" +
                "                \"1789\",\n" +
                "                \"1492\",\n" +
                "                \"il fantasmino Carmelo\"\n" +
                "            ],\n" +
                "            \"RispostaCorretta\": 3\n" +
                "        },\n" +
                "        {\n" +
                "            \"TestoDomanda\": \"Quanto fa 7 x 7\",\n" +
                "            \"Risposte\": [\n" +
                "                \"77\",\n" +
                "                \"49\",\n" +
                "                \"17\",\n" +
                "                \"il fantasmino Carmelo\"\n" +
                "            ],\n" +
                "            \"RispostaCorretta\": 2\n" +
                "        }\n" +
                "    ]\n" +
                "}";


        @GetMapping("/dyh")
        @CrossOrigin(origins = "http://localhost:8080")
        public String doYourHomework() {
            return response;
        }


}
