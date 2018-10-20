using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Transform ball;

    [Range(0, 1)]
    public float difficulty=0.5f;
	// Update is called once per frame
	void Update () {

        Vector3 newPos = transform.localPosition;
        newPos.x = Mathf.Lerp(transform.localPosition.x,ball.localPosition.x, difficulty);
        transform.localPosition = newPos;
	}
}
