using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_RoomDirection : MonoBehaviour
{
    public Vector2Int Position { get; set; }
    public RG_RoomDirection(Vector2Int startPos)
    {
        Position = startPos;
    }

    public Vector2Int Move(Dictionary<RG_Direction, Vector2Int> directionMovementMap)
    {
        RG_Direction toMove = (RG_Direction)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];
        return Position;
    }
}
