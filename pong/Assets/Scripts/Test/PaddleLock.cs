using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(transform.localPosition.x,0.5f, transform.localPosition.z);
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }
}
