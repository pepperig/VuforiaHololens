using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class network : MonoBehaviour {

	// Use this for initialization
	void Start () {

       
    }

    private void onCompleteDone(Action<UnityWebRequest> result, UnityWebRequest www) {

        //Debug.Log("COMPLETED");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Show results as text
            //Debug.Log(www.downloadHandler.text);
            //result(www.downloadHandler.text);
            result(www);
        }

        return;
    }

    public void GET(string url, Action<UnityWebRequest> result)
    {

        UnityWebRequest www = UnityWebRequest.Get(url);
        UnityWebRequestAsyncOperation asyncop =  www.SendWebRequest();
        asyncop.completed+= (AsyncOperation) => onCompleteDone(result, www);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
