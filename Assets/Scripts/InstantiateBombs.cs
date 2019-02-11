using UnityEngine;
using UnityEngine.Tilemaps;

public class InstantiateBombs : MonoBehaviour
{
    public static InstantiateBombs Instance;

    public Tilemap gameBoardTileMap;
    public GameObject bombPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void bombSpawner(Vector3 pos)
    {
        Vector3Int cell = gameBoardTileMap.WorldToCell(pos);
        Vector3 cellCentrePos = gameBoardTileMap.GetCellCenterWorld(cell);

        Instantiate(bombPrefab, cellCentrePos, Quaternion.identity);
    }
}
