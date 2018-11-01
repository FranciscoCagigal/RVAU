using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int player1Score, player2Score;
    public Text txtPlayer1Score, txtPlayer2Score;

    public void InscrementScore(string colliderName) {
        switch (colliderName)
        {
            case "Bounds South":
                player1Score++;
                txtPlayer1Score.text = "Player 1: " + player1Score;
                break;
            case "Bounds North":
                player2Score++;
                if (ApplicationModel.gameType == "cpuVSplayer")
                    txtPlayer2Score.text = "CPU: " + player2Score;
                else if(ApplicationModel.gameType == "playerVSplayer")
                    txtPlayer2Score.text = "Player 2: " + player2Score;
                break;

        }
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
