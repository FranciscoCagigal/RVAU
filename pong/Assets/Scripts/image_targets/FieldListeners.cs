using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FieldListeners : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    private BallMovement _ballScript;

    // Use this for initialization
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
            if (component.name == "Ball")
            {
                _ballScript = component.GetComponent<BallMovement>();
                if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
                    _ballScript.changeState(true);
                else if (newStatus == TrackableBehaviour.Status.NO_POSE)
                    _ballScript.changeState(false);
            }
    }
}
