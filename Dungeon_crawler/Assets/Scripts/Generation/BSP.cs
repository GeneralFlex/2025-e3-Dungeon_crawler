using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BSP : MonoBehaviour
{
    [Header("Rooms settings")]
    [SerializeField] private BoundsInt floorSize;
    [SerializeField] private int minRoomSize = 10;
    [SerializeField] private int maxRoomSize = 15;
    [SerializeField] private int margin = 1;
    [SerializeField] private bool shift = true;
    [SerializeField, Range(0, 100)] private int percentToEliminate = 25;
    [SerializeField] private int minimumRooms = 5;
    private List<BoundsInt> rooms = new List<BoundsInt>();

    public List<BoundsInt> GetRooms()
    {
        return rooms;
    }
    public List<BoundsInt> GenerateRooms()
    {
        return GenerateRooms(floorSize, minRoomSize, maxRoomSize, margin, percentToEliminate, shift);
    }
    private List<BoundsInt> GenerateRooms(BoundsInt floor, int MinRoomSize, int MaxRoomSize, int Margin, int PercentToEliminate, bool Shift)
    {
        int safetyCap = 10000;
        int safetyCounter = 0;
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(floor);
        while (roomsQueue.Count > 0 && safetyCounter<safetyCap)
        {
            safetyCounter++;
            BoundsInt room = roomsQueue.Dequeue();
            List<BoundsInt> splitRooms = new List<BoundsInt>();
            if (Random.value > 0.5f)
            {
                splitRooms = SplitY(room, MinRoomSize, MaxRoomSize);
            }
            else
            {
                splitRooms = SplitX(room, MinRoomSize, MaxRoomSize);
            }

            foreach (BoundsInt splitRoom in splitRooms)
            {
                if (splitRoom.size.x > MaxRoomSize || splitRoom.size.y > MaxRoomSize)
                {
                    roomsQueue.Enqueue(splitRoom);
                }
                else
                {
                    roomsList.Add(splitRoom);
                }
            }
        }
        int toEliminate = Mathf.FloorToInt(roomsList.Count * (PercentToEliminate / 100f));
        for (int i = 0; i < toEliminate; i++)
        {
            int rndIndex = Random.Range(0, roomsList.Count);
            roomsList.RemoveAt(rndIndex);
        }
        for (int i = 0; i < roomsList.Count; i++)
        {
            int shiftX = Shift ? Random.Range(-Margin, Margin + 1) : 0;
            int shiftY = Shift ? Random.Range(-Margin, Margin + 1) : 0;

            BoundsInt room = roomsList[i];

            room.min += new Vector3Int(Margin + shiftX, Margin + shiftY, 0);
            room.size -= new Vector3Int(Margin * 2, Margin * 2, 0);

            roomsList[i] = room;
        }
        if(roomsList.Count < minimumRooms)
        {
            Debug.LogWarning($"Generated rooms ({roomsList.Count}) less than minimum required ({minimumRooms}). Regenerating...");
            return GenerateRooms(floor, MinRoomSize, MaxRoomSize, Margin, PercentToEliminate, Shift);
        }
        rooms = roomsList;
        Debug.Log($"Generated {roomsList.Count} rooms");
        return roomsList;
    }

    private List<BoundsInt> SplitY(BoundsInt room, int MinRoomSize, int MaxRoomSize)
    {
        if (room.size.y <= MaxRoomSize && room.size.x <= MaxRoomSize)
            return new List<BoundsInt>() { room };
        if (room.size.y < 2*MinRoomSize-1)
        {
            if (room.size.x < 2 * MinRoomSize-1)
                return new List<BoundsInt>() { room };
            else
                return SplitX(room, MinRoomSize, MaxRoomSize);
        }
        List<BoundsInt> rooms = new List<BoundsInt>();

        BoundsInt room1 = new BoundsInt();
        BoundsInt room2 = new BoundsInt();
        int maxSplit = room.size.y - MinRoomSize;
        if (maxSplit <= MinRoomSize) maxSplit = MinRoomSize + 1;
        int rndSplit = Random.Range(MinRoomSize, maxSplit);
        room1.min = room.min;
        room1.size = new Vector3Int(room.size.x, rndSplit, 0);
        room2.min = new Vector3Int(room.min.x, room.min.y + rndSplit, 0);
        room2.size = new Vector3Int(room.size.x, room.size.y - rndSplit, 0);
        rooms.Add(room1);
        rooms.Add(room2);

        return rooms;
    }

    private List<BoundsInt> SplitX(BoundsInt room, int MinRoomSize, int MaxRoomSize)
    {
        if (room.size.y <= MaxRoomSize && room.size.x <= MaxRoomSize)
            return new List<BoundsInt>() { room };
        if (room.size.x < 2 * MinRoomSize-1)
        {
            if (room.size.y < 2 * MinRoomSize-1)
                return new List<BoundsInt>() {room};
            else
                return SplitY(room, MinRoomSize, MaxRoomSize);
        }
        List<BoundsInt> rooms = new List<BoundsInt>();

        BoundsInt room1 = new BoundsInt();
        BoundsInt room2 = new BoundsInt();
        int maxSplit = room.size.x - MinRoomSize;
        if (maxSplit <= MinRoomSize) maxSplit = MinRoomSize + 1;
        int rndSplit = Random.Range(MinRoomSize, maxSplit);
        room1.min = room.min;
        room1.size = new Vector3Int(rndSplit, room.size.y, 0);
        room2.min = new Vector3Int(room.min.x + rndSplit, room.min.y, 0);
        room2.size = new Vector3Int(room.size.x - rndSplit, room.size.y, 0);
        rooms.Add(room1);
        rooms.Add(room2);

        return rooms;
    }
}
