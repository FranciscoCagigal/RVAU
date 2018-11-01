using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Ability1Listener : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public BallMovement _ballScript;

    // Use this for initialization
    void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.UNDEFINED && previousStatus == TrackableBehaviour.Status.TRACKED)
        {
            switch (ApplicationModel.gameMode)
            {
                case "Normal":
                    return;
                case "Switcheroo":
                    switcherooMethod();
                    return;
                case "Rotation":
                    rotationMethod();
                    return;
            }
        }
    }

    private void switcherooMethod()
    {
        _ballScript.switcheroo();
    }

    private void rotationMethod()
    {
        if (gameObject.name == "Player1AbilityTarget")
            _ballScript.rotatePaddle(1);
        else if (gameObject.name == "Player2AbilityTarget")
            _ballScript.rotatePaddle(2);
    }
}
