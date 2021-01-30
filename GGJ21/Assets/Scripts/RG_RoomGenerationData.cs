using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomGenerationData.asset", menuName = "RoomGenerationData/Room Data")]
public class RG_RoomGenerationData : ScriptableObject
{
    public int numDirections;
    public int minIterations;
    public int maxIterations;
}
