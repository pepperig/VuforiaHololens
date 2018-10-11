using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {

    public GameObject camera;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = (transform.position - camera.transform.position).normalized;
        Quaternion quat = Quaternion.LookRotation(direction);
        Vector3 rot = new Vector3(0, quat.eulerAngles.y, 0);
       

       // transform.position = camera.transform.position + new Vector3(0, 0, 2);

        transform.eulerAngles = rot;
    }
}
