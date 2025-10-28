using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Visualiser : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase pathTile;

    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap pathTilemap;



    //room visualisation - BoundsInt
    public void VisualiseRooms(List<BoundsInt> bounds)
    {
        foreach (BoundsInt bound in bounds)
        {
            for (int x = bound.xMin; x < bound.xMax; x++)
            {
                for (int y = bound.yMin; y < bound.yMax; y++)
                {
                    floorTilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
                }
            }
        }
    }

    //wall visualisation
    public void VisualiseWalls(HashSet<Vector2Int> positions)
    {
        foreach (Vector2Int pos in positions.Distinct())
        {
            wallTilemap.SetTile((Vector3Int)pos, wallTile);
        }
    }

    //path visualisation (+rooms if HashSet not BoundsInt)
    public void VisualisePaths(HashSet<Vector2Int> positions)
    {
        foreach (Vector2Int pos in positions.Distinct())
        {
            pathTilemap.SetTile((Vector3Int)pos, pathTile);
        }
    }

    //clear
    public void ClearAll()
    {
        floorTilemap?.ClearAllTiles();
        pathTilemap?.ClearAllTiles();
        wallTilemap?.ClearAllTiles();
    }
}
