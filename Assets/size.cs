using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class size : MonoBehaviour {

	// Use this for initialization
	void Start () {

        
        Debug.Log("SIZE "+ gameObject.name + GetComponent<Renderer>().bounds.size);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
