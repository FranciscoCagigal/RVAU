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
                Debug.Log("SCORE " + player1Score);
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

        if(ApplicationModel.resumedGame){
            ApplicationModel.lastGameType = null;
            ApplicationModel.lastGameMode = null;
            ApplicationModel.lastAIdificulty = 0;
            ApplicationModel.lastGoals = 0;
            ApplicationModel.resumedGame = false;
            ApplicationModel.score1 = 0;
            ApplicationModel.score2 = 0;
        }
        

    }

    public int getPlayer1Score()
    {
        return player1Score;
    }

    public int getPlayer2Score()
    {
        return player2Score;
    }

    public void backToMenuWithState()
    {
        ApplicationModel.lastGameType = ApplicationModel.gameType;
        ApplicationModel.lastGameMode = ApplicationModel.gameMode;
        ApplicationModel.lastAIdificulty = ApplicationModel.AIdificulty;
        ApplicationModel.lastGoals = ApplicationModel.goals;
        ApplicationModel.score1 = player1Score;
        ApplicationModel.score2 = player2Score;
        ApplicationModel.resumedGame = false;
        SceneManager.LoadScene("Menu");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void init()
    {
        if (ApplicationModel.resumedGame)
        {
            ApplicationModel.gameType = ApplicationModel.lastGameType;
            ApplicationModel.gameMode = ApplicationModel.lastGameMode;
            ApplicationModel.AIdificulty = ApplicationModel.lastAIdificulty;
            ApplicationModel.goals = ApplicationModel.lastGoals;
            
            player1Score = ApplicationModel.score1;
            player2Score = ApplicationModel.score2;
            txtPlayer1Score.text = "Player 1: " + player1Score;
            if (ApplicationModel.gameType == "cpuVSplayer")
                txtPlayer2Score.text = "CPU: " + player2Score;
            else if (ApplicationModel.gameType == "playerVSplayer")
                txtPlayer2Score.text = "Player 2: " + player2Score;
        }
        
}
}
