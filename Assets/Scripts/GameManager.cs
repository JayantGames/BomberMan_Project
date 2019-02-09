﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
                               
    public GameObject bombPrefab;
    public List<GameObject> bombsSpawnList;
    public bool detonateBomb;

    public bool player1Life;
    public bool player2Life;

    public List<GameObject> powerUpPrefabsList;
    public List<PrefabTile> powerUpTilesList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player1Life = true;
        player2Life = true;
    }


    public void spawnBombs(Vector3 position)
    {
        if (PowerUpsManager.Instance.getCurrentPowerUp() == PowerUpsManager.PowerUps.MORE_BOMBS)
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
        // Vector3 worldPos = Camera.main.WorldToScreenPoint(transform.position);

        PowerUpsManager.Instance.currPowerUp = PowerUpsManager.PowerUps.NONE;
        Vector3Int cell = MapDestroyer.Instance.tileMap.WorldToCell(transform.position);
        Vector3 cellCentrePos = MapDestroyer.Instance.tileMap.GetCellCenterWorld(cell);

        StartCoroutine(MapDestroyer.Instance.instantiateRandomPowerUp(cellCentrePos));
    }
}
