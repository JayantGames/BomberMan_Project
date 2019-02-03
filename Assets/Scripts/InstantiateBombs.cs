using UnityEngine;
using UnityEngine.Tilemaps;

public class InstantiateBombs : MonoBehaviour
{
  //  public Camera camera;

    public Tilemap gameBoardTileMap;
    public GameObject bombPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = gameBoardTileMap.WorldToCell(worldPos);
            Vector3 cellCentrePos = gameBoardTileMap.GetCellCenterWorld(cell);

            Instantiate(bombPrefab, cellCentrePos, Quaternion.identity);
        }
    }
}
