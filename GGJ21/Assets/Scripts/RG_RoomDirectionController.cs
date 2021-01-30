using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_RoomDirectionController : MonoBehaviour
{
    public static List<Vector2Int> roomsVisited = new List<Vector2Int>();
    private static readonly Dictionary<RG_Direction, Vector2Int> directionMovementMap = new Dictionary<RG_Direction, Vector2Int>
    {
        {RG_Direction.top, Vector2Int.up},
        {RG_Direction.left, Vector2Int.left},
        {RG_Direction.down, Vector2Int.down},
        {RG_Direction.right, Vector2Int.right}
    };

    public static List<Vector2Int> GenerateRoom(RG_RoomGenerationData roomData)
    {
        List<RG_RoomDirection> rooms = new List<RG_RoomDirection>();

        for (int i = 0; i < roomData.numDirections; i++)
        {
            rooms.Add(new RG_RoomDirection(Vector2Int.zero));
        }

        int iterations = Random.Range(roomData.minIterations, roomData.maxIterations);

        for (int i = 0; i < iterations; i++)
        {
            foreach (RG_RoomDirection roomDirection in rooms)
            {
                Vector2Int newPositions = roomDirection.Move(directionMovementMap);
                roomsVisited.Add(newPositions);
            }
        }
        return roomsVisited;
    }
}

