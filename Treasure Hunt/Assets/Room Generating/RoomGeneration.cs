﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const int HoechstanzahlRaeume = 10;
    public const int Seed1 = 43;
    //Groesse der Raeume (Breite und Länge)
    public const int raumgroesse = 17;
}

public class RoomGeneration : MonoBehaviour
{
    public List<Room> raumListe = new List<Room>();

    public GameObject RaumMitVierTueren;
    public GameObject RaumMitDreiTueren;
    public GameObject RaumMitNebeneinanderLiegendenTueren;
    public GameObject RaumMitHintereinanderLiegendenTueren;
    public GameObject RaumMitEinerTuer;
    public GameObject[] raumArtenArray;

    //speichert die relative Position der generierten Räume und zeigt an, welche Positionen belegt sind
    //o: die Position ist leer
    //x: die Position ist belegt
    private char[,] generierteRaeume;

    //speichert die Koordinaten der generierten Räume, der mittlerste Raum ist in der Mitte des Arrays
    private char[,] raumPositionen;

    //speichert die Positionen, an denen die Türen platziert werden müssen
    public List<Vector3> tuerpositionen;

    private Vector3 center;
    private Vector3 size;
    public bool randomizeSeed = true;

    private int raumzaehler = 0;

    // Use this for initialization
    void Start()
    {
        //Raum 1: Raum mit Vier Tueren
        //Raum 2: Raum mit Zwei hintereinanderliegenden Tueren (gerader Durchgang)
        //Raum 3: Raum mit Drei Tueren
        //Raum 4: Raum mit Zwei nebeneinanderliegenden Tueren (Ecke)
        //Raum 5: Raum mit einer Tuer (quasi Dead End)

        raumArtenArray = new GameObject[5];
        raumArtenArray[0] = RaumMitVierTueren;
        raumArtenArray[1] = RaumMitHintereinanderLiegendenTueren;
        raumArtenArray[2] = RaumMitDreiTueren;
        raumArtenArray[3] = RaumMitNebeneinanderLiegendenTueren;
        raumArtenArray[4] = RaumMitEinerTuer;

        center = new Vector3(0, -1, 0);

        if (randomizeSeed == false)
        {
            Random.InitState(Constants.Seed1);
        }

        //raumPositionen = new char[Constants.HoechstanzahlRaeume + (Constants.HoechstanzahlRaeume / 2), Constants.HoechstanzahlRaeume + (Constants.HoechstanzahlRaeume / 2)];
        //Positionen der Raeume ohne Koordinaten
        generierteRaeume = new char[Constants.HoechstanzahlRaeume, Constants.HoechstanzahlRaeume];
        tuerpositionen = new List<Vector3>();

        for (int i = 0; i < Constants.HoechstanzahlRaeume; i++)
        {
            for (int j = 0; j < Constants.HoechstanzahlRaeume; j++)
            {
                generierteRaeume[i, j] = 'o';
            }
        }

        while (raumzaehler < Constants.HoechstanzahlRaeume) {
            FindRoomPosition(); 
            raumzaehler++;
        }

        for (int i = 0; i < Constants.HoechstanzahlRaeume; i++) {

            SpawnRoom(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FindRoomPosition()
    {
        bool raumPositionGefunden = false;
        Vector3 pos = center;

        while (raumPositionGefunden == false && raumzaehler != 0) {
            int zufallsraumKoordinateX = Random.Range(0, Constants.HoechstanzahlRaeume - 1);
            int zufallsraumKoordinateZ = Random.Range(0, Constants.HoechstanzahlRaeume - 1);

            if (generierteRaeume[zufallsraumKoordinateX, zufallsraumKoordinateZ] == 'o') {
                if ((generierteRaeume[zufallsraumKoordinateX - 1, zufallsraumKoordinateZ] != 'o') ||
                    (generierteRaeume[zufallsraumKoordinateX, zufallsraumKoordinateZ - 1] != 'o') ||
                    (generierteRaeume[zufallsraumKoordinateX + 1, zufallsraumKoordinateZ] != 'o') ||
                    (generierteRaeume[zufallsraumKoordinateX, zufallsraumKoordinateZ + 1] != 'o')
                    ) {
                    raumPositionGefunden = true;
                }
            }
            pos = new Vector3((float)zufallsraumKoordinateX, 0, (float)zufallsraumKoordinateZ);
        }
        
        generierteRaeume[(int)pos.x + Constants.HoechstanzahlRaeume/2, (int)pos.y + Constants.HoechstanzahlRaeume/2] = 'x';

    }

    void SpawnRoom(int uebergebeneRaumNummer)
    {
        Room raum = new Room();
        raum.raumNummer = uebergebeneRaumNummer;
        raumListe.Add(raum);
    }

    int CreateDoor(float xKoordinate, float zKoordinate) {
        Vector3 neuePosition = new Vector3(xKoordinate, -1, zKoordinate);
        
        //Wenn es die Tür schon von einem anderen Raum aus gibt 
        //wird sie nicht in das Array eingefügt damit sie nicht doppelt eingefügt wird
        if (tuerpositionen.Contains(neuePosition))
        {
            //1 = quasi Errorcode
            return (1);
        }
        else {
            tuerpositionen.Add(neuePosition);
            return (0);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

}

public class Room {
    public Vector3 raumPosition;
    public int raumNummer;

    bool hatRaumDrueber = false;
    bool hatRaumRechts = false;
    bool hatRaumUnten = false;
    bool hatRaumLinks = false;

    void setRaumDrueberTrue() {
        hatRaumDrueber = true;
    }

    void setRaumRechtsTrue() {
        hatRaumRechts = true;
    }

    void setRaumUntenTrue() {
        hatRaumUnten = true;
    }

    void setRaumLinksTrue() {
        hatRaumLinks = true;
    }

}
