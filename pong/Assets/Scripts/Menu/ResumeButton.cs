using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ApplicationModel.lastGameType == null)
        {
            Debug.Log("entrei aqui " + ApplicationModel.lastGameType);
            //gameObject.GetComponent<Button>().enabled=false;
            gameObject.SetActive(false);
        }
        else gameObject.SetActive(true);
    }
}
