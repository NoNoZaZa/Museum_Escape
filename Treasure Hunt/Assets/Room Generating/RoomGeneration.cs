﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{

    public GameObject raumPrefab;
    Vector3 raumposition = new Vector3(0, 0, 0);
    int successRaeume;
    int successWaende;
    public static int maximaleRaumzahl = 10;
    public int maximaleRaumzahlPublic = maximaleRaumzahl;

    public bool pseudorandom = false;
    public static int seed = 42;

    int hilfsvariableNachbarfindung = 0;
    //
    int raumnummer = 0;

    //x = 0 speichert die Position der Waende, x = 1 speichert die durch die Länge des Vektors codierte Rotation der Waende
    public Vector3[,] wandpositionenArray = new Vector3[maximaleRaumzahl * 4, 2];

    public List<Vector3> raumpositionen = new List<Vector3>();
    Vector3 norden = new Vector3(-34, 0, 0);
    Vector3 osten = new Vector3(0, 0, 34);
    Vector3 sueden = new Vector3(34, 0, 0);
    Vector3 westen = new Vector3(0, 0, -34);

    public List<Vector3> tuerpositionen = new List<Vector3>();

    //1 = norden, 2 = osten, 3 = sueden, 4 = westen
    int naechsteRaumPosition = 0;

    // Use this for initialization
    void Start()
    {

        Debug.Log("Die RoomGeneration wurde gestartet");

        if (pseudorandom) {
            Random.InitState(seed);
        }

        while (successRaeume < maximaleRaumzahl)
        {
            successRaeume = successRaeume + RaumGenerieren();
            //Debug.Log(success);
        }

        //Raumpositionen durchgehen und Raeume mit Nachbarraeumen finden
        foreach (var raumposition in raumpositionen)
        {
            if (raumpositionen.Contains(raumposition + norden))
            {
                wandpositionenArray[hilfsvariableNachbarfindung, 1] = new Vector3(5, 0, 0);
                //Debug.Log("Raum Nummer " + hilfsvariableNachbarfindung + " hat einen Nachbarn in -x Richtung!");
                tuerpositionen.Add(wandpositionenArray[hilfsvariableNachbarfindung, 0]);
                //jeder zweite Vektor3 in tuerpositionen traegt die Raumnummer um besser vergleichen zu können, ob es ein Quizraum ist
                tuerpositionen.Add(new Vector3(raumnummer,0,0));
            }
            if (raumpositionen.Contains(raumposition + osten))
            {
                wandpositionenArray[hilfsvariableNachbarfindung + 1, 1] = new Vector3(5, 0, 0);
                tuerpositionen.Add(wandpositionenArray[hilfsvariableNachbarfindung + 1, 0]);
                tuerpositionen.Add(new Vector3(raumnummer, 0, 0));

            }
            if (raumpositionen.Contains(raumposition + sueden))
            {
                wandpositionenArray[hilfsvariableNachbarfindung + 2, 1] = new Vector3(5, 0, 0);
                tuerpositionen.Add(wandpositionenArray[hilfsvariableNachbarfindung + 2, 0]);
                tuerpositionen.Add(new Vector3(raumnummer, 0, 0));

            }
            if (raumpositionen.Contains(raumposition + westen))
            {
                wandpositionenArray[hilfsvariableNachbarfindung + 3, 1] = new Vector3(5, 0, 0);
                tuerpositionen.Add(wandpositionenArray[hilfsvariableNachbarfindung + 3, 0]);
                tuerpositionen.Add(new Vector3(raumnummer, 0, 0));

            }

            hilfsvariableNachbarfindung = hilfsvariableNachbarfindung + 4;
            raumnummer++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    int RaumGenerieren()
    {
        int zufallszahl = Random.Range(0, 100);

        //Debug.Log("zufallszahl: " + zufallszahl);
        //Debug.Log("naechsteRaumPosition am Anfang: " + naechsteRaumPosition);


        if (zufallszahl > 25)
        {
            naechsteRaumPosition = 2;
            //Debug.Log("naechsteRaumPosition nach > 25 Vergleich: " + naechsteRaumPosition);

            if (zufallszahl > 50)
            {
                naechsteRaumPosition = 3;
                //Debug.Log("naechsteRaumPosition nach > 50 Vergleich: " + naechsteRaumPosition);

                if (zufallszahl > 75)
                {
                    naechsteRaumPosition = 4;
                    //Debug.Log("naechsteRaumPosition nach > 75 Vergleich: " + naechsteRaumPosition);

                }
            }
        }
        else if (zufallszahl < 25)
        {
            naechsteRaumPosition = 1;
            //Debug.Log("naechsteRaumPosition nach < 25 Vergleich: " + naechsteRaumPosition);

        }

        switch (naechsteRaumPosition)
        {
            case 1:
                raumposition = new Vector3(raumposition.x + norden.x, raumposition.y + norden.y, raumposition.z + norden.z);
                break;
            case 2:
                raumposition = new Vector3(raumposition.x + osten.x, raumposition.y + osten.y, raumposition.z + osten.z);
                break;
            case 3:
                raumposition = new Vector3(raumposition.x + sueden.x, raumposition.y + sueden.y, raumposition.z + sueden.z);
                break;
            case 4:
                raumposition = new Vector3(raumposition.x + westen.x, raumposition.y + westen.y, raumposition.z + westen.z);
                break;

        }

        if (raumpositionen.Contains(raumposition))
        {
            //Raum existiert bereits an der Stelle, return 0 als "Fehler"
            return 0;
        }
        else
        {
            if (raumpositionen.Contains(new Vector3(0, 0, 0)))
            {
                GameObject raum = Instantiate(raumPrefab, raumposition, Quaternion.identity);
                raum.tag = "Raum";
                //Debug.Log(raum);
                raumpositionen.Add(raumposition);
            }
            else
            {
                //Sicherstellen, dass ein Raum an den Ursprungskoordinaten existiert
                raumposition = new Vector3(0, 0, 0);
                GameObject raum = Instantiate(raumPrefab, raumposition, Quaternion.identity);
                raum.tag = "Raum";
                raumpositionen.Add(raumposition);
            }


            int hilfsvariableWandpositionen = successRaeume * 4;

            Vector3 wandpositionNorden = raumposition + new Vector3(-16.475f, 2.9f, 2);
            Vector3 wandpositionOsten = raumposition + new Vector3(2, 2.9f, 16.475f);
            Vector3 wandpositionSueden = raumposition + new Vector3(16.475f, 2.9f, 0);
            Vector3 wandpositionWesten = raumposition + new Vector3(0, 2.9f, -16.475f);

            wandpositionenArray[hilfsvariableWandpositionen, 0] = wandpositionNorden;
            wandpositionenArray[hilfsvariableWandpositionen, 1] = new Vector3(1, 0, 0);

            wandpositionenArray[hilfsvariableWandpositionen + 1, 0] = wandpositionOsten;
            wandpositionenArray[hilfsvariableWandpositionen + 1, 1] = new Vector3(2, 0, 0);

            wandpositionenArray[hilfsvariableWandpositionen + 2, 0] = wandpositionSueden;
            wandpositionenArray[hilfsvariableWandpositionen + 2, 1] = new Vector3(3, 0, 0);

            wandpositionenArray[hilfsvariableWandpositionen + 3, 0] = wandpositionWesten;
            wandpositionenArray[hilfsvariableWandpositionen + 3, 1] = new Vector3(4, 0, 0);


        }


        return 1;
    }

}
