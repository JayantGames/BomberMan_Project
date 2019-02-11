using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUISettings : MonoBehaviour
{
    public Text player1Score;
    public Text player2Score;
    public Text player1UIScore;
    public Text player2UIScore;
    public Text highScore;
    public Text MenuHeading;
    public Text levelTimer;
    public GameObject resumeButton;
    public GameObject player1RPB;
    public GameObject player2RPB;
    public GameObject gameOverPanel;
    public GameObject topUIPanel;


    public void pauseGame()
    {
        GameManager.Instance.paused = true;
    }

    public void resumeGame()
    {
        GameManager.Instance.paused = false;
    }

    public void goToMainMenu()
    {
        GameManager.Instance.goToMainMenu();
    }

    public void reloadLevel()
    {
        GameManager.Instance.restartLevel();
    }

    public void LateUpdate()
    {
        player1Score.text = "Player 1 : " + GameManager.Instance.player1Score.ToString();
        player2Score.text = "Player 2 : " + GameManager.Instance.player2Score.ToString();
        player1UIScore.text = "P1 Score : " + GameManager.Instance.player1Score.ToString();
        player2UIScore.text = "P2 Score : " + GameManager.Instance.player2Score.ToString();

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                {
                    highScore.text = "High Score : " + PlayerPrefs.GetString("Level1_HS");
                    break;
                }
            case "Level2":
                {
                    highScore.text = "High Score : " + PlayerPrefs.GetString("Level2_HS");
                    break;
                }
            case "Level3":
                {
                    highScore.text = "High Score : " + PlayerPrefs.GetString("Level3_HS");
                    break;
                }
            case "Level4":
                {
                    highScore.text = "High Score : " + PlayerPrefs.GetString("Level4_HS");
                    break;
                }
            case "Level5":
                {
                    highScore.text = "High Score : " + PlayerPrefs.GetString("Level5_HS");
                    break;
                }
        }

        if (GameManager.Instance.paused)
        {
            MenuHeading.text = "PAUSED";
        }
        else if (GameManager.Instance.gameOver)
        {
            if (GameManager.Instance.player1Alive && !GameManager.Instance.player2Alive)
            {
                MenuHeading.text = "Player 1 Won";
            }
            else if (!GameManager.Instance.player1Alive && GameManager.Instance.player2Alive)
            {
                MenuHeading.text = "Player 2 Won";
            }
            else
            {
                MenuHeading.text = "Draw";
            }
        }

        levelTimer.text = "Time Left : " + ((int)GameManager.Instance.levelTimer).ToString();
    }
}
