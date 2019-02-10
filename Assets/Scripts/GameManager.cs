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

    public bool player1Life;
    public bool player2Life;

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
    public int levelTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player1Life = true;
        player2Life = true;
    }

    private void LateUpdate()
    {    
        
        if (paused)
        {                      
            inGameSettingsPanel.transform.Find("GameOverMenu").GetComponent<Animator>().SetBool("Show",true);
            inGameSettingsPanel.GetComponent<InGameUISettings>().topUIPanel.SetActive(false);
        }
        else
        {                      
            levelTimer -= (int)Time.deltaTime;
            inGameSettingsPanel.transform.Find("GameOverMenu").GetComponent<Animator>().SetBool("Show",false);
            inGameSettingsPanel.GetComponent<InGameUISettings>().topUIPanel.SetActive(true);
        }

        if (gameOver)
        {
            paused = false;
            inGameSettingsPanel.GetComponent<InGameUISettings>().resumeButton.SetActive(false);
            inGameSettingsPanel.transform.Find("GameOverMenu").GetComponent<Animator>().SetBool("Show", true);   
        }
        else if(!paused)
        {
            inGameSettingsPanel.GetComponent<InGameUISettings>().resumeButton.SetActive(true);  
        }
    }


    public void spawnBombs(Vector3 position, PowerUpsManager.PowerUps powerUp)
    {
        if (powerUp == PowerUpsManager.PowerUps.MORE_BOMBS)
        {
            if (bombsSpawnList.Count < 2)
            {
                GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity) as GameObject;
              //  bomb.GetComponent<BombExplosion>().
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
