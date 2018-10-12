/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class NewBehaviourPalletScript : MonoBehaviour, ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNTIY_MONOBEHAVIOUR_METHODS
    private Vector3 orientation;
    private GameObject boxHandler;
    public GameObject message;
    WorldAnchorStore store = null;
    public GameObject pallet;
    private bool istracked = false;

    private void StoreLoaded(WorldAnchorStore store)
    {
        this.store = store;
        Debug.Log("ANCOR STORE SAVED");
    }

    protected virtual void Start()
    {
        // Debug.Log("SIZE "+ GetComponent<Renderer>().bounds.size);

        //get initial orientation
        orientation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        boxHandler = GameObject.FindGameObjectsWithTag("BoxHandler")[0];

        //VuforiaBehaviour.Instance.enabled = false;

        WorldAnchorStore.GetAsync(StoreLoaded);
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    protected virtual void Update()
    {
        if (istracked)
        {
            transform.eulerAngles = orientation;
        }
        //Debug.Log(orientation);
    }

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {

        //Debug.Log("PEPPE found");

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

            if (mTrackableBehaviour.TrackableName == "marker_2_2")
            {
                string state = boxHandler.GetComponent<BoxHandler>().currstate;
                if (state == "waitpallet")
                {
                    OnTrackingFound();
                    boxHandler.GetComponent<BoxHandler>().currstate = "detectpallet";
                }

            }
            /*if (TrackerManager.Instance.GetTracker<PositionalDeviceTracker>() != null)
                TrackerManager.Instance.GetTracker<PositionalDeviceTracker>().Stop();
            //Rotational DeviceTracker
            if (TrackerManager.Instance.GetTracker<RotationalDeviceTracker>() != null)
                TrackerManager.Instance.GetTracker<RotationalDeviceTracker>().Stop();*/

            //Object Tracker
            /* if (TrackerManager.Instance.GetTracker<ObjectTracker>() != null)
                 TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();*/

        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            // OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            // OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    protected virtual void OnTrackingFound()
    {
        boxHandler.GetComponent<BoxHandler>().timerStart = true;
        message.GetComponent<TextMesh>().text = "SCANNED OK";
        message.transform.position = transform.position;
        GetComponent<ImageTargetBehaviour>().enabled = false;

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);


        //pallet.GetComponent<MeshRenderer>().enabled = true;

        // Enable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        //OnTrackingLost();

        //TODO TRIAL

        //WorldAnchor anchor = gameObject.AddComponent<WorldAnchor>();
        //bool savedAnchor = false;
        //if (!savedAnchor) // only save this once
        //{
        //    if (this.store != null)
        //    {
        //        savedAnchor = this.store.Save("PalletAnchor", anchor);
        //        if (!savedAnchor)
        //        {
        //            // Anchor failed to save to the store.
        //            // Handle errors here.
        //            Debug.Log("ANCH" + anchor.isLocated);
        //        }
        //        else {
        //            GetComponent<ImageTargetBehaviour>().enabled = false;
        //        }
        //    }
        //}


        //constant x,z variable y
        orientation.y = transform.eulerAngles.y;
        transform.eulerAngles = orientation;

        istracked = true;

        // if (mTrackableBehaviour)
        //     mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PRIVATE_METHODS
}