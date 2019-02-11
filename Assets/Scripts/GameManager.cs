using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject bombPrefab;
    public List<GameObject> bombsSpawnList;
    public bool detonateBomb;

    public bool player1Alive;
    public bool player2Alive;

    public List<GameObject> powerUpPrefabsList;
    public int player1Score;
    public int player2Score;
    public GameObject deathPrefab;
    public CharacterController.PLAYER playerBomb;
    public GameObject inGameSettingsPanel;
    public bool paused;
    public bool gameOver;

    public bool player1Powerup;
    public bool player2Powerup;
    public float levelTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player1Alive = true;
        player2Alive = true;
    }

    private void LateUpdate()
    {

        if (paused)
        {
            inGameSettingsPanel.transform.Find("GameOverMenu").GetComponent<Animator>().SetBool("Show", true);
            inGameSettingsPanel.GetComponent<InGameUISettings>().topUIPanel.SetActive(false);
        }
        else if (gameOver)
        {
            paused = false;
            inGameSettingsPanel.GetComponent<InGameUISettings>().resumeButton.SetActive(false);
            inGameSettingsPanel.transform.Find("GameOverMenu").GetComponent<Animator>().SetBool("Show", true);
            inGameSettingsPanel.GetComponent<InGameUISettings>().topUIPanel.SetActive(false);
        }
        else
        {
            if (!gameOver)
            {
                levelTimer -= Time.deltaTime;
            }
            inGameSettingsPanel.transform.Find("GameOverMenu").GetComponent<Animator>().SetBool("Show", false);
            inGameSettingsPanel.GetComponent<InGameUISettings>().topUIPanel.SetActive(true);
        }


        if (levelTimer <= 0)
        {
            gameOver = true;
            if (player1Score > player2Score)
            {
                saveScoreInPlayerPrefs(player1Score.ToString());
            }
            else
            {
                saveScoreInPlayerPrefs(player2Score.ToString());
            }
        }

        if (!player1Alive || !player2Alive)
        {
            gameOver = true;
            if (player1Score > player2Score)
            {
                saveScoreInPlayerPrefs(player1Score.ToString());
            }
            else
            {
                saveScoreInPlayerPrefs(player2Score.ToString());
            }
        }
    }

    public void saveScoreInPlayerPrefs(string score)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                {
                    if (Convert.ToInt32(PlayerPrefs.GetString("Level1_HS")) < Convert.ToInt32(score))
                    {
                        PlayerPrefs.SetString("Level1_HS", score);
                    }  
                    break;
                }
            case "Level2":
                {
                    if (Convert.ToInt32(PlayerPrefs.GetString("Level2_HS")) < Convert.ToInt32(score))
                    {
                        PlayerPrefs.SetString("Level2_HS", score);
                    }
                    break;
                }
            case "Level3":
                {
                    if (Convert.ToInt32(PlayerPrefs.GetString("Level3_HS")) < Convert.ToInt32(score))
                    {
                        PlayerPrefs.SetString("Level3_HS", score);
                    }
                    break;
                }
            case "Level4":
                {
                    if (Convert.ToInt32(PlayerPrefs.GetString("Level4_HS")) < Convert.ToInt32(score))
                    {
                        PlayerPrefs.SetString("Level4_HS", score);
                    }
                    break;
                }
            case "Level5":
                {
                    if (Convert.ToInt32(PlayerPrefs.GetString("Level5_HS")) < Convert.ToInt32(score))
                    {
                        PlayerPrefs.SetString("Level5_HS", score);
                    }
                    break;
                }
        }   
    }


    public void spawnBombs(Vector3 position, PowerUpsManager.PowerUps powerUp)
    {
        if (powerUp == PowerUpsManager.PowerUps.MORE_BOMBS)
        {
            if (bombsSpawnList.Count < 2)
            {
                GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity) as GameObject;
                bombsSpawnList.Add(bomb);
            }
        }
        else
        {
            if (bombsSpawnList.Count < 1)
            {
                GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity) as GameObject;
                bombsSpawnList.Add(bomb);
            }
        }

    }

    public void bombDestroyed()
    {
        Vector3Int cell = MapDestroyer.Instance.tileMap.WorldToCell(transform.position);
        Vector3 cellCentrePos = MapDestroyer.Instance.tileMap.GetCellCenterWorld(cell);

        StartCoroutine(MapDestroyer.Instance.instantiateRandomPowerUp(cellCentrePos));
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
