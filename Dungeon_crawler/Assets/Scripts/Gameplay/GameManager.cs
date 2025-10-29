using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private DungeonManager dungeonManager;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (dungeonManager.StartRoom == null)
        {
            Debug.LogError("startroom is null");
            return;
        }
        Vector2 spawnPos = dungeonManager.StartRoom.center;
        Instantiate(playerPrefab, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        Debug.Log("player spawned at: " + spawnPos);
    }
}
