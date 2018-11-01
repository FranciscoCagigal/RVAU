using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    public int player1Score, player2Score;
    public Text txtPlayer1Score, txtPlayer2Score;
    public TextMeshProUGUI WinnerLabel, Player1Label, Player2Label, Score1Label, Score2Label;
    public GameObject gameOverPanel;

    public bool InscrementScore(string colliderName) {
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

        if (player1Score == ApplicationModel.goals || player2Score == ApplicationModel.goals)
        {
            gameOver();
            return false;
        }

        return true;
    }

    private void gameOver()
    { 
        int winner; //1 if player 1 wins, 0 if player 2 wins

        if (player1Score > player2Score)
            winner = 1;
        else winner = 0;

        if (winner == 1)
            WinnerLabel.GetComponent<TextMeshProUGUI>().text = "PLAYER1 WINS!!!";

        Player1Label.GetComponent<TextMeshProUGUI>().text = "Player1";
        Score2Label.GetComponent<TextMeshProUGUI>().text = player2Score.ToString();
        Score1Label.GetComponent<TextMeshProUGUI>().text = player1Score.ToString();

        if (ApplicationModel.gameType == "cpuVSplayer")
        {
            Player2Label.GetComponent<TextMeshProUGUI>().text = "CPU";

            if (winner == 0)
                WinnerLabel.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        }
        else if (ApplicationModel.gameType == "playerVSplayer")
        {
            Player2Label.GetComponent<TextMeshProUGUI>().text = "Player2";

            if (winner == 0)
                WinnerLabel.GetComponent<TextMeshProUGUI>().text = "PLAYER2 WINS!!!";
        }

        gameOverPanel.SetActive(true);

    }

    public int getPlayer1Score()
    {
        return player1Score;
    }

    public int getPlayer2Score()
    {
        return player2Score;
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
