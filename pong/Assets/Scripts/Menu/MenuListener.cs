using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MenuListener : MonoBehaviour {

    List<string> modes = new List<string>() {"Normal","Switcheroo","Rotation"};

    public void Start1v1()
    {
        ApplicationModel.gameType = "playerVSplayer";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartPlayerVcpu()
    {
        ApplicationModel.gameType = "cpuVSplayer";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void changeGameMode(int index) {
        ApplicationModel.gameMode = modes[index];
    }

    public void changeAIDifficulty(float value)
    {
        ApplicationModel.AIdificulty = value;
    }

    public void changeGoalLimit(string value)
    {
        if(value == "")
        {
            ApplicationModel.goals = 1;
            return;
        }

        int limit = Convert.ToInt32(value);
        if (limit >= 10)
            ApplicationModel.goals = 10;
        else if(limit<=0)
            ApplicationModel.goals = 1;
        else ApplicationModel.goals = limit;

        Debug.Log(gameObject.name);
    }
}
