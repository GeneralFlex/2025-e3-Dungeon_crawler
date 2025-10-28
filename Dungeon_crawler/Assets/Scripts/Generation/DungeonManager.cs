using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private BSP bsp;
    [SerializeField] private PathGenerator pathGen;
    [SerializeField] private WallGenerator wallGen;
    [SerializeField] private Visualiser visualiser;
    private List<GameObject> visualRooms = new List<GameObject>();
    private List<GameObject> visualPaths = new List<GameObject>();

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        //clear previous
        visualiser.ClearAll();

        //generate rooms
        bsp.GenerateRooms();
        List<BoundsInt> rooms = bsp.GetRooms();
        HashSet<Vector2Int> roomTiles = bsp.GetRoomTiles();

        BoundsInt floor = bsp.GetFloorSize();

        //generate paths
        HashSet<Vector2Int> corridors = pathGen.GeneratePaths(rooms);

        //generate walls
        HashSet<Vector2Int> tiles = new HashSet<Vector2Int>(roomTiles);
        tiles.UnionWith(corridors);
        HashSet<Vector2Int> walls = wallGen.GenerateWalls(tiles);

        //find empty tiles
        HashSet<Vector2Int> empty = new HashSet<Vector2Int>();
        for(int x = floor.min.x-20; x < floor.max.x+20; x++)
        {
            for (int y = floor.min.y-20; y < floor.max.y+20; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (!tiles.Contains(pos))
                {
                    empty.Add(pos);
                }
            }
        }

        //visualise rooms and paths
        visualiser.VisualisePaths(tiles);
        //visualiser.VisualiseWalls(walls);
        visualiser.VisualiseWalls(empty);

        //place walls everywhere

    }
}
