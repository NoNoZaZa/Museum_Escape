﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz4 : MonoBehaviour
{
    public GameObject rad1, rad2, rad3;


    // Use this for initialization
    void Start()
    {
        int zufall = (Random.Range(1, 8)) * 45;
        //GameObject radChild1 = rad1.transform.GetChild(0).Find("rad").gameObject;
        rad1 = Instantiate(rad1, rad1.transform.position, Quaternion.identity);
        rad1.transform.Rotate(0, zufall, 0);
        rad1.transform.parent = this.transform;
        if (!rad1.activeInHierarchy) rad1.SetActive(true);

        zufall = (Random.Range(1, 8)) * 45;
        rad2 = Instantiate(rad2, rad2.transform.position, Quaternion.identity);
        rad2.transform.Rotate(0, zufall, 0);
        rad2.transform.parent = this.transform;
        if (!rad2.activeInHierarchy) rad2.SetActive(true);

        zufall = (Random.Range(1, 8)) * 45;
        rad3 = Instantiate(rad3, rad3.transform.position, Quaternion.identity);
        rad3.transform.Rotate(0, zufall, 0);
        rad3.transform.parent = this.transform;
        if (!rad3.activeInHierarchy) rad3.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.tag == "rad")
                {
                    GameObject rad = hit.collider.gameObject;
                    float p = hit.point.x;
                    float radP = rad.transform.position.x;
                    //Rechtsdrehung
                    if (p > radP)
                    {
                        float rot = rad.transform.rotation.z;
                        rad.transform.Rotate(0, (rot + 45), 0);
                    }
                    //Linkssdrehung
                    if (p < radP)
                    {
                        float rot = rad1.transform.rotation.z;
                        rad.transform.Rotate(0, (rot - 45), 0);
                    }
                }




                //if (right)
                //{
                //    float rot1 = rad1.transform.rotation.z;
                //    rad1.transform.Rotate(0, (rot1 + 45), 0);

                //    float rot2 = rad2.transform.rotation.z;
                //    rad2.transform.Rotate(0, (rot2 + 45), 0);

                //    float rot3 = rad3.transform.rotation.z;
                //    rad3.transform.Rotate(0, (rot3 + 45), 0);
                //}
                //if (left)
                //{
                //    float rot1 = rad1.transform.rotation.z;
                //    rad1.transform.Rotate(0, (rot1 - 45), 0);

                //    float rot2 = rad2.transform.rotation.z;
                //    rad2.transform.Rotate(0, (rot2 - 45), 0);

                //    float rot3 = rad3.transform.rotation.z;
                //    rad3.transform.Rotate(0, (rot3 - 45), 0);
                //}
            }
        }
    }
}
