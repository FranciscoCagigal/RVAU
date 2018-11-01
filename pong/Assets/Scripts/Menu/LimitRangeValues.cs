using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LimitRangeValues : MonoBehaviour {

    public int max, min;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<TextMeshProUGUI>().text = "10";
    }

    // Update is called once per frame
    void Update() {
        Debug.Log("dsfwegawreg " + gameObject.GetComponent<TextMeshProUGUI>().text + " ");
        if (gameObject.GetComponent<TextMeshProUGUI>().text == null)
        {
            Debug.Log("saiiiiiiiiiii");
            return;
        }

        try
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = Mathf.Clamp((float)Convert.ToInt32(gameObject.GetComponent<TextMeshProUGUI>().text), max, min).ToString();

        }
        catch (FormatException e)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "1";
        }
    }
}
