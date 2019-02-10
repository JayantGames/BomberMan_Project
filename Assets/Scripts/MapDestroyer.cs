using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;   
using System.Collections.Generic;

public class MapDestroyer : MonoBehaviour
{
    public static MapDestroyer Instance;

    public Tilemap tileMap;
    public Tile wallTile;
    public Tile destructibleTile;   

    public GameObject explosionPrefab;
    public GameObject coinPrefab;
    public bool randomInstantiation;
    public List<GameObject> randomlyInstantiatedPowerUp;

    public int tileValue = 10;

    private void Awake()
    {
        Instance = this;
    }



    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tileMap.WorldToCell(worldPos);
        ExplodeCell(originCell);
        Debug.Log("PowerUpsManager.Instance.getCurrentPowerUp() : " + PowerUpsManager.Instance.getCurrentPowerUp());

        if (ExplodeCell(originCell + new Vector3Int(1, 0, 0)))
        {
            if (PowerUpsManager.Instance.getCurrentPowerUp() == PowerUpsManager.PowerUps.LONG_BLAST)
            {
                ExplodeCell(originCell + new Vector3Int(2, 0, 0));
            }
         }


        if (ExplodeCell(originCell + new Vector3Int(0, 1, 0)))
        {
            if (PowerUpsManager.Instance.getCurrentPowerUp() == PowerUpsManager.PowerUps.LONG_BLAST)
            {
                ExplodeCell(originCell + new Vector3Int(0, 2, 0));
            }
        }


        if (ExplodeCell(originCell + new Vector3Int(-1, 0, 0)))
        {
            if (PowerUpsManager.Instance.getCurrentPowerUp() == PowerUpsManager.PowerUps.LONG_BLAST)
            {
                ExplodeCell(originCell + new Vector3Int(-2, 0, 0));
            }
        }

        if (ExplodeCell(originCell + new Vector3Int(0, -1, 0)))
        {
            if (PowerUpsManager.Instance.getCurrentPowerUp() == PowerUpsManager.PowerUps.LONG_BLAST)
            {
                ExplodeCell(originCell + new Vector3Int(0, -2, 0));
            }
        }
    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tileMap.GetTile<Tile>(cell);

        if (tile == wallTile)
        {
            return false;
        }

        if (tile == destructibleTile)
        {
            //  Vector3 tilePos = tileMap.GetCellCenterWorld(cell);
            Vector3 tilePos = tileMap.GetCellCenterWorld(cell);        
            Instantiate(coinPrefab, tilePos, Quaternion.identity);
            tileMap.SetTile(cell, null);
            saveScore(tileValue);
        }

                 
        Vector3 pos = tileMap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);

        return true;
    }

    public void saveScore(int score)
    {
        if (GameManager.Instance.playerBomb == CharacterController.PLAYER.PLAYER_1)
        {
            GameManager.Instance.player1Score += score;
        }
        else
        {
            GameManager.Instance.player2Score += score;
        }
    }

    public GameObject returnRandomPowerUp()
    {
        return GameManager.Instance.powerUpPrefabsList[Random.Range(0, GameManager.Instance.powerUpPrefabsList.Count)];
    }

    public IEnumerator instantiateRandomPowerUp(Vector3 cell)
    {                                            
        yield return new WaitForSeconds(0.65f);

        if (Random.value > 0.1)
        {
            for (int i = 0; i < GameManager.Instance.powerUpPrefabsList.Count-1; i++)
            {                                             
                GameObject randomlyInstantiatedPowerUpInstance = Instantiate(returnRandomPowerUp(), cell, Quaternion.identity);
                randomlyInstantiatedPowerUp.Add(randomlyInstantiatedPowerUpInstance);
                randomInstantiation = true; 
            }
        }                                             
    }
}
