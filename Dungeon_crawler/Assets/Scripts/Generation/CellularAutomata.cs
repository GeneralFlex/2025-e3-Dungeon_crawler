using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
    [SerializeField][Range(0,100)] int noise;
    [SerializeField] int iterations;
    public HashSet<Vector2Int> GenerateCaves(BoundsInt room)
    {
        return GenerateCaves(room, noise, iterations);
    }

    private HashSet<Vector2Int> GenerateCaves(BoundsInt room, int noise, int iterations)
    {
        //false = empty, true = floor
        Dictionary<Vector2Int, bool> floorTiles = new Dictionary<Vector2Int, bool>();
        //initial noise
        for (int x = room.min.x; x < room.max.x; x++)
        {
            for (int y = room.min.y; y < room.max.y; y++)
            {
                if (Random.Range(0, 100) < noise)
                {
                    floorTiles.Add(new Vector2Int(x, y), true);  // floor
                }
                else
                {
                    floorTiles.Add(new Vector2Int(x, y), false); // empty
                }
            }
        }
        //list of directions
        List<Vector2Int> dir = new List<Vector2Int>
            {
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
                new Vector2Int(1, 1),
                new Vector2Int(1, -1),
                new Vector2Int(-1, 1),
                new Vector2Int(-1, -1)
            };

        //iterations
        for (int i = 0; i < iterations; i++)
        {
            Dictionary<Vector2Int,bool> newFloorTiles = new Dictionary<Vector2Int, bool>(floorTiles);
            foreach (Vector2Int tile in floorTiles.Keys.ToList())
            {
                int wallCount = 0;
                foreach (Vector2Int d in dir)
                {
                    Vector2Int nb = tile + d;
                    if (!floorTiles.ContainsKey(nb) || floorTiles[nb] == false)  //false = wall
                        wallCount++;
                }

                newFloorTiles[tile] = wallCount < 5;
            }
            floorTiles = new Dictionary<Vector2Int, bool>(newFloorTiles);
        }

    HashSet<Vector2Int> finalTiles = new HashSet<Vector2Int>();
        foreach (Vector2Int tile in floorTiles.Keys)
        {
            if (floorTiles[tile] == true)
            {
                finalTiles.Add(tile);
            }
        }

        return finalTiles;
    }
}
