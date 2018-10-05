/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES
    GameObject box1, boxHandler;
    public GameObject message;

    #region UNTIY_MONOBEHAVIOUR_METHODS
    private Vector3 orientation;

    protected virtual void Start()
    {
        // Debug.Log("SIZE "+ GetComponent<Renderer>().bounds.size);

        //get initial orientation
        orientation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        box1 = GameObject.FindGameObjectsWithTag("Box1")[0];
        boxHandler = GameObject.FindGameObjectsWithTag("BoxHandler")[0];
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    protected virtual void Update()
    {
        // transform.eulerAngles = orientation;
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


        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");


            if (mTrackableBehaviour.TrackableName == "Astronaut")
            {
                string state = boxHandler.GetComponent<BoxHandler>().currstate;
                if (state == "waitbox1")
                {
                    boxHandler.GetComponent<BoxHandler>().currstate = "detectbox1";
                    OnTrackingFound();
                }
                else if (state == "placingbox1") { OnTrackingFound(); }
                else
                {
                    //text.text = "WRONG BOX";
                    message.GetComponent<Text>().text = "WRONG BOX";
                    boxHandler.GetComponent<BoxHandler>().timerStart = true;
                }
            }

            if (mTrackableBehaviour.TrackableName == "Drone")
            {
                string state = boxHandler.GetComponent<BoxHandler>().currstate;
                if (state == "waitbox2")
                {
                    boxHandler.GetComponent<BoxHandler>().currstate = "detectbox2";
                    OnTrackingFound();
                }
                else if (state == "placingbox2") { OnTrackingFound(); }
                else
                {
                    //text.text = "WRONG BOX";
                    message.GetComponent<Text>().text = "WRONG BOX";
                    boxHandler.GetComponent<BoxHandler>().timerStart = true;
                }
            }

            if (mTrackableBehaviour.TrackableName == "Fissure")
            {
                string state = boxHandler.GetComponent<BoxHandler>().currstate;
                if (state == "waitbox3")
                {
                    boxHandler.GetComponent<BoxHandler>().currstate = "detectbox3";
                    OnTrackingFound();
                }
                else if (state == "placingbox3") { OnTrackingFound(); }
                else
                {
                    //text.text = "WRONG BOX";
                    message.GetComponent<Text>().text = "WRONG BOX";
                    boxHandler.GetComponent<BoxHandler>().timerStart = true;
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
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    protected virtual void OnTrackingFound()
    {

        boxHandler.GetComponent<BoxHandler>().timerStart = true;
        message.GetComponent<Text>().text = "SCANNED OK";


        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Enable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = true;

        ////Enable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        //TODO TRIAL

        transform.eulerAngles = orientation;

        // if (mTrackableBehaviour)
        //     mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PRIVATE_METHODS
}