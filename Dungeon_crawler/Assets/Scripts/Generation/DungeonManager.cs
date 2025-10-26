using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private BSP bsp;
    [SerializeField] private PathGenerator pathGen;
    [SerializeField] private Visualiser visualiser;
    private List<GameObject> visualRooms = new List<GameObject>();
    private List<GameObject> visualPaths = new List<GameObject>();

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        //destroy previous visuals
        foreach (GameObject obj in visualRooms)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        foreach (GameObject obj in visualPaths)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        //generate rooms
        List<BoundsInt> rooms = bsp.GenerateRooms();
        
        //generate paths
        List<Vector2Int> corridors = pathGen.GeneratePaths(rooms);

        //visalise
        visualRooms = visualiser.VisualiseTiles(rooms);
        visualPaths = visualiser.VisualiseTilesPath(corridors);
    }
}
