//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using HoloToolkit.Unity;
//using UnityEngine.UI;
//using System.Net.Sockets;
//using System.Net;
//using System;

//public class cameraStream : MonoBehaviour {

//    //public GameObject image;
//    RenderTexture txt;
//    Texture2D txt2D;
//    //RawImage rimage;
//    Camera camera;



//    byte[] data = new byte[0];
//    int sent;
//    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("192.168.1.180"), 11000);

//    Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//    // Use this for initialization
//    void Start () {

//        txt = new RenderTexture(256, 256, 0);
//        camera = GetComponent<Camera>();
//        //rimage =  image.GetComponent<RawImage>();
//        camera.targetTexture = txt;
//        txt2D =  new Texture2D(256,256);

//        try
//        {
//            server.Connect(ipep);
//        }
//        catch (SocketException e)
//        {
//            Debug.Log(e.ToString());
//        }


//    }
	
//	// Update is called once per frame
//	IEnumerator Update () {

//        RenderTexture.active = txt;
//        txt2D.ReadPixels(new Rect(0, 0, txt.width, txt.height), 0, 0);
//        txt2D.Apply();
//        //rimage.texture = txt2D;


//        //sent = SendVarData(server, txt2D.EncodeToPNG());
//        StartCoroutine( SendVarData(server, txt2D.GetRawTextureData()));

//        yield return null;

//    }


//    private static IEnumerator  SendVarData(Socket s, byte[] data)
//    {
//        int total = 0;
//        int size = data.Length;
//        int dataleft = size;
//        int sent;

//        byte[] datasize = new byte[0];
//        datasize = BitConverter.GetBytes(size);
//        sent = s.Send(datasize);

//        while (total < size)
//        {
//            sent = s.Send(data, total, dataleft, SocketFlags.None);
//            total += sent;
//            dataleft -= sent;
//            yield return null;
//        }

//        yield return null;
//    }
//}
