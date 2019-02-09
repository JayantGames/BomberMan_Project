using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class MapDestroyer : MonoBehaviour
{
    public static MapDestroyer Instance;

    public Tilemap tileMap;
    public Tile wallTile;
    public Tile destructibleTile;   

    public GameObject explosionPrefab;
    public bool randomInstantiation;
    public GameObject randomlyInstantiatedPowerUp;

    private void Awake()
    {
        Instance = this;
    }

    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tileMap.WorldToCell(worldPos);
        ExplodeCell(originCell);

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
            tileMap.SetTile(cell, null);
        }

        if (Random.value > 0.1)
        {
            for (int i = 0; i < GameManager.Instance.powerUpTilesList.Count; i++)
            {
                if (tile == GameManager.Instance.powerUpTilesList[i])
                {
                    tileMap.SetTile(cell, returnRandomPowerUp());
                    randomInstantiation = true;
                }
            }
        }

        Vector3 pos = tileMap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);

        if (randomlyInstantiatedPowerUp != null)
        {
            StartCoroutine(reTagRandomInstantiatedPowerUp());
        }

        return true;
    }

    public PrefabTile returnRandomPowerUp()
    {
        return GameManager.Instance.powerUpTilesList[Random.Range(0, GameManager.Instance.powerUpTilesList.Count)];
    }

    public IEnumerator reTagRandomInstantiatedPowerUp()
    {
        yield return new WaitForSeconds(3f);
        randomlyInstantiatedPowerUp.tag = "PowerUp";
    }
}
