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
}
