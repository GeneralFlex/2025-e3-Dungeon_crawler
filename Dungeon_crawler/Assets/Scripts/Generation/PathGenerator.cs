using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathGenerator : MonoBehaviour
{
    public List<Vector2Int> GeneratePaths(List<BoundsInt> rooms)
    {
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        Dictionary<Vector2Int, BoundsInt> centerToRoom = new Dictionary<Vector2Int, BoundsInt>();
        foreach (BoundsInt room in rooms)
        {
            Vector2Int center = new Vector2Int(
                Mathf.FloorToInt(room.center.x),
                Mathf.FloorToInt(room.center.y)
            );
            roomCenters.Add(center);
            centerToRoom.Add(center, room);
        }
        List<Vector2Int> corridors = new List<Vector2Int>();
        Vector2Int currentRoom = roomCenters[0];
        while (roomCenters.Count > 0)
        {
            roomCenters.Remove(currentRoom);
            Vector2Int closestRoom = FindClosestRoom(currentRoom, roomCenters);
            roomCenters.Remove(closestRoom);
            List<Vector2Int> path = CreatePath(currentRoom, closestRoom);
            corridors.AddRange(path);
            currentRoom = closestRoom;
        }
        return corridors;
    }

    private List<Vector2Int> CreatePath(Vector2Int currentRoom, Vector2Int closestRoom)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int tempPos = currentRoom;

        if(tempPos.y - closestRoom.y < 1) {
            tempPos.y += 1;
        }
        else if (tempPos.y - closestRoom.y > 1)
        {
            tempPos.y -= 1;
        }
        if(tempPos.x - closestRoom.x < 1)
        {
            tempPos.x += 1;
        }
        else if (tempPos.x - closestRoom.x > 1)
        {
            tempPos.x -= 1;
        }

        while (tempPos.x != closestRoom.x)
        {
            if (closestRoom.x > tempPos.x)
            {
                tempPos.x += 1;
            }
            else if (closestRoom.x < tempPos.x)
            {
                tempPos.x -= 1;
            }
            path.Add(new Vector2Int(tempPos.x, tempPos.y));
        }
        while (tempPos.y != closestRoom.y)
        {
            if (closestRoom.y > tempPos.y)
            {
                tempPos.y += 1;
            }
            else if (closestRoom.y < tempPos.y)
            {
                tempPos.y -= 1;
            }
            path.Add(new Vector2Int(tempPos.x, tempPos.y));
        }
        return path;
    }

    private Vector2Int FindClosestRoom(Vector2Int currentRoom, List<Vector2Int> roomCenters)
    {
        Vector2Int closestRoom = roomCenters[0];
        float closestDistance = Vector2Int.Distance(currentRoom, closestRoom);
        foreach (Vector2Int room in roomCenters)
        {
            float distance = Vector2Int.Distance(currentRoom, room);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRoom = room;
            }
        }
        return closestRoom;
    }
}
