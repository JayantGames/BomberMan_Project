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
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Vector3Int cell = gameBoardTileMap.WorldToCell(worldPos);
        Vector3 cellCentrePos = gameBoardTileMap.GetCellCenterWorld(cell);

        Instantiate(bombPrefab, cellCentrePos, Quaternion.identity);
    }                                                       

   /* private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = gameBoardTileMap.WorldToCell(worldPos);
            Vector3 cellCentrePos = gameBoardTileMap.GetCellCenterWorld(cell);

            Instantiate(bombPrefab, cellCentrePos, Quaternion.identity);
        }
    }  */
}
