using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_RoomGenerator : MonoBehaviour
{
    public RG_RoomGenerationData roomGenerationData;
    private List<Vector2Int> newRooms;

    void Start()
    {
        newRooms = RG_RoomDirectionController.GenerateRoom(roomGenerationData);
        SpawnRooms(newRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RG_RoomController.instance.LoadRoom("Room1", 0, 0);
        foreach (Vector2Int roomCoords in rooms)
        {
            RG_RoomController.instance.LoadRoom("Room1", roomCoords.x, roomCoords.y);
        }
    }
}
