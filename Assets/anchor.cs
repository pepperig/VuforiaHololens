using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;


public class anchor : MonoBehaviour {

    WorldAnchorStore store = null;
    public bool save = false;

    private void StoreLoaded(WorldAnchorStore store)
    {
        this.store = store;
        Debug.Log("ANCOR STORE SAVED");
    }

    // Use this for initialization
    void Start () {


        WorldAnchorStore.GetAsync(StoreLoaded);
    }
	
	// Update is called once per frame
	void Update () {

        if (!save) return;

        save = false;
        WorldAnchor anchor = gameObject.GetComponent<WorldAnchor>();

        if (anchor != null)
            DestroyImmediate(anchor);


        anchor = gameObject.AddComponent<WorldAnchor>();
        Debug.Log("anchor value " + anchor);
        bool savedAnchor = false;
        if (!savedAnchor) // only save this once
        {
            if (this.store != null)
            {
                store.Delete(gameObject.name);

                savedAnchor = this.store.Save(gameObject.name, anchor);
                if (!savedAnchor)
                {
                    // Anchor failed to save to the store.
                    // Handle errors here.
                    Debug.Log("ANCH" + anchor.isLocated);
                }
                else
                {
                    Debug.Log("ANCHor saved" + anchor.isLocated);
                    // GetComponent<ImageTargetBehaviour>().enabled = false;
                }
            }
        }

    }
}
