using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RG_RoomGenerator : MonoBehaviour
{
    public RG_RoomGenerationData roomGenerationData;
    private List<Vector2Int> newRooms;

    [SerializeField]
    private int numberOfRooms = 1;

    private string[] roomNames = new string[]
    {
        "Room1",
        "Room2",
        "Room3",
        "Room4",
        "Room5",
        "Room6",
        "Room7",
        "Room8",
        "Room9",
        "Room10",
        "Room11",
        "Room12",
        "Room13",
        "Room14"
    };

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
            RG_RoomController.instance.LoadRoom(
                roomNames[Random.Range(0, roomNames.Count())],
                roomCoords.x, roomCoords.y);
        }
    }
}
