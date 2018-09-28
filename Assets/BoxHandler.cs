﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandler : MonoBehaviour {

    //public List<GameObject> vec = new List<GameObject>();
    public GameObject box1, box2, box3, marker1, marker2, marker3;
    public string currstate, nextstate;
    private const float delta = 0.02f;
	// Use this for initialization
	void Start () {

        // box1.active = false;
        //box2.active = false;
        currstate = "waitpallet";
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("MARKER" + marker1.transform.position.x);
        //Debug.Log("BOX x" + box1.transform.position.x);
        //Debug.Log("BOX y" + box1.transform.position.y);
        //Debug.Log("BOX z" + box1.transform.position.z);

        //Debug.Log("MAGNITUDE" + marker1.transform.position.magnitude);
        if (currstate == "detectpallet")
        {
            currstate = "waitbox1";
        }

        if (currstate == "detectbox1") {

            box1.GetComponent<MeshRenderer>().enabled = true;
            currstate = "placingbox1";
        }


        if (currstate == "placingbox1")
        {
            //Debug.Log("IN RANGE");
            //box1.active = false;
            bool alignx = false;
            bool aligny = false;
            bool alignz = false;
            if ((marker1.transform.position.x <= box1.transform.position.x + delta) && (marker1.transform.position.x >= box1.transform.position.x - delta)) alignx = true; else alignx = false;
            if ((marker1.transform.position.y <= box1.transform.position.y + delta) && (marker1.transform.position.y >= box1.transform.position.y - delta)) aligny = true; else aligny = false;
            if ((marker1.transform.position.z <= box1.transform.position.z + delta) && (marker1.transform.position.z >= box1.transform.position.z - delta)) alignz = true; else alignz = false;

            if (alignx && aligny && alignz)
            {
                box1.GetComponent<MeshRenderer>().enabled = false;
                currstate = "waitbox2";
            }
        }
        else if (currstate == "placingbox1"){

            box1.GetComponent<MeshRenderer>().enabled = true;
            //box1.active = true;
        }


        if (currstate == "detectbox2")
        {

            box2.GetComponent<MeshRenderer>().enabled = true;
            currstate = "placingbox2";
        }


        if (currstate == "placingbox2")
        {
            //Debug.Log("IN RANGE");
            //box1.active = false;
            bool alignx = false;
            bool aligny = false;
            bool alignz = false;
            if ((marker2.transform.position.x <= box2.transform.position.x + delta) && (marker2.transform.position.x >= box2.transform.position.x - delta)) alignx = true; else alignx = false;
            if ((marker2.transform.position.y <= box2.transform.position.y + delta) && (marker2.transform.position.y >= box2.transform.position.y - delta)) aligny = true; else aligny = false;
            if ((marker2.transform.position.z <= box2.transform.position.z + delta) && (marker2.transform.position.z >= box2.transform.position.z - delta)) alignz = true; else alignz = false;

            if (alignx && aligny && alignz)
            {
                box2.GetComponent<MeshRenderer>().enabled = false;
                currstate = "waitbox3";
            }
           
        }
        else if (currstate == "placingbox2")
        {

            box2.GetComponent<MeshRenderer>().enabled = true;
            //box1.active = true;
        }



        if (currstate == "detectbox3")
        {

            box3.GetComponent<MeshRenderer>().enabled = true;
            currstate = "placingbox3";
        }


        if (currstate == "placingbox3")
        {
            //Debug.Log("IN RANGE");
            //box1.active = false;
            bool alignx = false;
            bool aligny = false;
            bool alignz = false;
            if ((marker3.transform.position.x <= box3.transform.position.x + delta) && (marker3.transform.position.x >= box3.transform.position.x - delta)) alignx = true; else alignx = false;
            if ((marker3.transform.position.y <= box3.transform.position.y + delta) && (marker3.transform.position.y >= box3.transform.position.y - delta)) aligny = true; else aligny = false;
            if ((marker3.transform.position.z <= box3.transform.position.z + delta) && (marker3.transform.position.z >= box3.transform.position.z - delta)) alignz = true; else alignz = false;

            if (alignx && aligny && alignz)
            {
                box3.GetComponent<MeshRenderer>().enabled = false;
                currstate = "waitbox4";
            }

        }
        else if (currstate == "placingbox3")
        {

            box3.GetComponent<MeshRenderer>().enabled = true;
            //box1.active = true;
        }


    }
}
