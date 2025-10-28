using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public HashSet<Vector2Int> GenerateWalls(HashSet<Vector2Int> floorTiles)
    {
        HashSet<Vector2Int> wallTiles = new HashSet<Vector2Int>();

        List<Vector2Int> dir = new List<Vector2Int>();
        //list of directions
        dir.Add(new Vector2Int(1, 0));
        dir.Add(new Vector2Int(-1, 0));
        dir.Add(new Vector2Int(0, 1));
        dir.Add(new Vector2Int(0, -1));


        //if neighbor tile isnt in hashset of tiles, its a wall
        foreach (Vector2Int tile in floorTiles)
        {
            foreach (Vector2Int d in dir)
            {
                Vector2Int nb = tile + d;
                if (!floorTiles.Contains(nb))
                {
                    wallTiles.Add(nb);
                }
            }
        }

        return wallTiles;
    }
}
