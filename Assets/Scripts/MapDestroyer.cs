using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public static MapDestroyer Instance;

    public Tilemap tileMap;
    public Tile wallTile;
    public Tile destructibleTile;

    public GameObject explosionPrefab;

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
            ExplodeCell(originCell + new Vector3Int(2, 0, 0));
        }


        if (ExplodeCell(originCell + new Vector3Int(0, 1, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(0, 2, 0));
        }


        if (ExplodeCell(originCell + new Vector3Int(-1, 0, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(-2, 0, 0));
        }

        if (ExplodeCell(originCell + new Vector3Int(0, -1, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(0, -2, 0));
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

        Vector3 pos = tileMap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);

        return true;
    }
}
