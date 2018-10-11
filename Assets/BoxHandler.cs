using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Networking;
using System.IO;
using System;

public class BoxHandler : MonoBehaviour {

    //public List<GameObject> vec = new List<GameObject>();
    public GameObject box1, box2, box3, marker1, marker2, marker3, message, boxtoskip, markertoskip;
    public GameObject item1, item2, item3, item4;
    public string currstate, nextstate;
    private const float delta = 0.02f;
    public bool timerStart = false;
    public float timeLeft = 2.0f;
    public GameObject network;
    public bool skip=false;

    string[] data1 = new string[] { "G0465", "1/2", "76 x 43 x 24 cm", "21 Kg", "6" };
    string[] data2 = new string[] { "G0466", "1/2", "50 x 43 x 24 cm", "10 Kg", "7" };
    string[] data3 = new string[] { "G0467", "1/2", "40 x 30 x 24 cm", "21 Kg", "8" };
    string[] data4 = new string[] { "G0468", "1/2", "20 x 43 x 24 cm", "10 Kg", "9" };

    // Use this for initialization
    void Start () {

        // box1.active = false;
        //box2.active = false;
        currstate = "waitpallet";

        GameObject t = item1.transform.GetChild(0).transform.GetChild(0).gameObject; 
        //for (int i = 0; i < t.transform.GetChildCount(); i++) {

        //    t.transform.GetChild(i).gameObject.GetComponent<Text>().text = "ciaoo";
        //}
        t.transform.GetChild(0).gameObject.GetComponent<Text>().text = data1[0];
        t.transform.GetChild(1).gameObject.GetComponent<Text>().text = data1[1];
        t.transform.GetChild(2).gameObject.GetComponent<Text>().text = data1[2];
        t.transform.GetChild(3).gameObject.GetComponent<Text>().text = data1[3];
        t.transform.GetChild(4).gameObject.GetComponent<Text>().text = data1[4];

        //item2
        t = item2.transform.GetChild(0).transform.GetChild(0).gameObject;
        t.transform.GetChild(0).gameObject.GetComponent<Text>().text = data2[0];
        t.transform.GetChild(1).gameObject.GetComponent<Text>().text = data2[1];
        t.transform.GetChild(2).gameObject.GetComponent<Text>().text = data2[2];
        t.transform.GetChild(3).gameObject.GetComponent<Text>().text = data2[3];
        t.transform.GetChild(4).gameObject.GetComponent<Text>().text = data2[4];

        //item3
        t = item3.transform.GetChild(0).transform.GetChild(0).gameObject;
        t.transform.GetChild(0).gameObject.GetComponent<Text>().text = data3[0];
        t.transform.GetChild(1).gameObject.GetComponent<Text>().text = data3[1];
        t.transform.GetChild(2).gameObject.GetComponent<Text>().text = data3[2];
        t.transform.GetChild(3).gameObject.GetComponent<Text>().text = data3[3];
        t.transform.GetChild(4).gameObject.GetComponent<Text>().text = data3[4];

        //item4
        t = item4.transform.GetChild(0).transform.GetChild(0).gameObject;
        t.transform.GetChild(0).gameObject.GetComponent<Text>().text = data4[0];
        t.transform.GetChild(1).gameObject.GetComponent<Text>().text = data4[1];
        t.transform.GetChild(2).gameObject.GetComponent<Text>().text = data4[2];
        t.transform.GetChild(3).gameObject.GetComponent<Text>().text = data4[3];
        t.transform.GetChild(4).gameObject.GetComponent<Text>().text = data4[4];

    }

    void updateData(GameObject item, string code, string np, string size, string weight, string n) {

        GameObject t = item.transform.GetChild(0).transform.GetChild(0).gameObject;
        t.transform.GetChild(0).gameObject.GetComponent<Text>().text = code;
        t.transform.GetChild(1).gameObject.GetComponent<Text>().text = np;
        t.transform.GetChild(2).gameObject.GetComponent<Text>().text = size;
        t.transform.GetChild(3).gameObject.GetComponent<Text>().text = weight;
        t.transform.GetChild(4).gameObject.GetComponent<Text>().text = n;

    }
	// Update is called once per frame
	void Update () {
        //Debug.Log("MARKER" + marker1.transform.position.x);
        //Debug.Log("BOX x" + box1.transform.position.x);
        //Debug.Log("BOX y" + box1.transform.position.y);
        //Debug.Log("BOX z" + box1.transform.position.z);

        //Debug.Log("MAGNITUDE" + marker1.transform.position.magnitude);

        if (timerStart)
        {
            //Debug.Log("TIMER START");
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                //text.text = "";
                message.GetComponent<TextMesh>().text = "";
                timerStart = false;
                //Debug.Log("TIMER STOP");
            }
        }
        else
        {

            timeLeft = 2.0f;
        }


        if (currstate == "detectpallet")
        {
            currstate = "waitbox1";
        }

        if (currstate == "detectbox1") {

            network.GetComponent<network>().GET("http://192.168.1.173:8000?box=1", (UnityWebRequest h) => {

                Debug.Log(h.downloadHandler.text);

            });

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

                updateData(item1, data2[0], data2[1], data2[2], data2[3], data2[4]);
                updateData(item2, data3[0], data3[1], data3[2], data3[3], data3[4]);
                updateData(item3, data4[0], data4[1], data4[2], data4[3], data4[4]);
                item4.SetActive(false);

            }
        }
        //else if (currstate == "placingbox1"){

        //    box1.GetComponent<MeshRenderer>().enabled = true;
        //    box1.active = true;
        //}


        if (currstate == "detectbox2")
        {

            box2.GetComponent<MeshRenderer>().enabled = true;
            currstate = "placingbox2";
            network.GetComponent<network>().GET("http://192.168.1.173:8000?box=2", (UnityWebRequest h) => {

                Debug.Log(h.downloadHandler.text);

            });
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
                currstate = "waitboxtoskip";
                updateData(item1, data3[0], data3[1], data3[2], data3[3], data3[4]);
                updateData(item2, data4[0], data4[1], data4[2], data4[3], data4[4]);
                item3.SetActive(false);
            }
           
        }
        //else if (currstate == "placingbox2")
        //{

        //    box2.GetComponent<MeshRenderer>().enabled = true;
        //    box1.active = true;
        //}

        if (currstate == "detectboxtoskip")
        {

            boxtoskip.GetComponent<MeshRenderer>().enabled = true;
            currstate = "placingboxtoskip";
            network.GetComponent<network>().GET("http://192.168.1.173:8000?box=3", (UnityWebRequest h) => {

                Debug.Log(h.downloadHandler.text);

            });
        }

        if (currstate == "placingboxtoskip")
        {

            if (skip) {

                currstate = "waitbox3";
                boxtoskip.GetComponent<MeshRenderer>().enabled = false;
                skip = false;
                updateData(item1, data4[0], data4[1], data4[2], data4[3], data4[4]);
                item2.SetActive(false);
            }

        }

        if (currstate == "detectbox3")
        {

            box3.GetComponent<MeshRenderer>().enabled = true;
            currstate = "placingbox3";
            network.GetComponent<network>().GET("http://192.168.1.173:8000?box=4", (UnityWebRequest h) => {

                Debug.Log(h.downloadHandler.text);

            });
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
                item1.SetActive(false);
            }

        }
        else if (currstate == "placingbox3")
        {

            box3.GetComponent<MeshRenderer>().enabled = true;
            //box1.active = true;
        }

        skip = false;
    }
}
