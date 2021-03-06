﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dekoplatzierung : MonoBehaviour {

    public RoomGeneration raumgenerator;
    public QuizGeneratorSkript quizgenerator;
    public GameObject pharaonenkopf;
    public GameObject pharaonenstatue;
    public GameObject pharaonenstatueOhneKopf;
    public GameObject kobra;

    List<Vector3> dekopositionen = new List<Vector3>();

    int zaehlvariable = 0;

	// Use this for initialization
	void Start () {
        int[] positionenarray = new int[] {-13, -11, -9, -7, -5, 5, 7, 9, 11, 13 };

        foreach (var raumposition in raumgenerator.raumpositionen) {
            //Random.InitState((int)System.DateTime.Now.Ticks);
            int dekoanzahl = Random.Range(2, 7);
            while (dekoanzahl > 0) {

                Vector3 dekoposition = new Vector3(raumposition.x + positionenarray[Random.Range(0, 9)], 0.5f, raumposition.z + positionenarray[Random.Range(0, 9)]);
                if (!dekopositionen.Contains(dekoposition)) {
                    int dekoelement = Random.Range(1, 5);
                    switch (dekoelement)
                    {
                        case 1:
                            Instantiate(pharaonenkopf, dekoposition, Quaternion.Euler(0, Random.Range(0, 361), 0));
                            break;
                        case 2:
                            Instantiate(pharaonenstatue, new Vector3(dekoposition.x, 1.9f, dekoposition.z), Quaternion.Euler(270, Random.Range(0, 361), 0));
                            break;
                        case 3:
                            Instantiate(pharaonenstatueOhneKopf, new Vector3(dekoposition.x, 1.9f, dekoposition.z), Quaternion.Euler(270, Random.Range(0, 361), 0));
                            break;
                        case 4:
                            Instantiate(kobra, new Vector3(dekoposition.x, 1.5f, dekoposition.z), Quaternion.Euler(270, Random.Range(0, 361), 0));
                            break;
                    }
                    dekoanzahl--;
                }
            }
            zaehlvariable++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
