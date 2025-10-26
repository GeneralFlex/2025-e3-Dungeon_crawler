using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Visualiser : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    

    //OBO = One By One

    //room visualiser
    public List<GameObject> VisualiseTiles(List<BoundsInt> bounds)
    {
        return InstantiateTiles(bounds);
    }

    private List<GameObject> InstantiateTiles(List<BoundsInt> bounds)
    {
        List<GameObject> tiles = new List<GameObject>();
        int index = 0;
        foreach (BoundsInt bound in bounds)
        {
            index++;
            GameObject room = Instantiate(tilePrefab, bound.center, Quaternion.identity);
            room.transform.localScale = new Vector3(bound.size.x, bound.size.y, 1);
            room.name = "Room_" + index;
            tiles.Add(room);
        }
        return tiles;
    }

    //path visualiser
    public List<GameObject> VisualiseTilesPath(List<Vector2Int> positions)
    {
        return InstantiateTilesPath(positions);
    }

    private List<GameObject> InstantiateTilesPath(List<Vector2Int> positions)
    {
        List<GameObject> paths = new List<GameObject>();
        foreach (Vector2Int pos in positions.Distinct())
        {
            GameObject path = Instantiate(tilePrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            path.name = "Path";
            paths.Add(path);
        }
        return paths;
    }

    //OBO visualiser
    public List<GameObject> VisualiseTilesOBO(List<BoundsInt> bounds)
    {
        List<GameObject> tiles = new List<GameObject>();
        foreach (BoundsInt bound in bounds)
        {
            List<Vector2> positions = new List<Vector2>();
            for (int x = bound.xMin; x < bound.xMax; x++)
            {
                for (int y = bound.yMin; y < bound.yMax; y++)
                {
                    positions.Add(new Vector2(x, y));
                }
            }
            tiles.AddRange(InstantiateTilesOBO(positions));
        }
        return tiles;
    }
    private List<GameObject> InstantiateTilesOBO(List<Vector2> positions)
    {
        List<GameObject> tiles = new List<GameObject>();
        foreach (Vector2 pos in positions.Distinct())
        {
            tiles.Add(Instantiate(tilePrefab, pos, Quaternion.identity));
        }
        return tiles;
    }
    
}
