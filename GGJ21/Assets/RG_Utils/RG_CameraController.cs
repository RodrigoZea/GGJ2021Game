using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_CameraController : MonoBehaviour
{
    public static RG_CameraController instance;
    public RG_Room currentRoom;
    public float roomSpeedChange = 100;
    
    void Awake() {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        ChangePosition();
    }

    public void ChangePosition()
    {
        if (currentRoom == null) return;

        Vector3 targetPos = GetCameraTargetPosition();
        transform.position = Vector3.MoveTowards(
          transform.position,
          targetPos,
          Time.deltaTime * roomSpeedChange
        );
    }

    public Vector3 GetCameraTargetPosition()
    {
        if (currentRoom == null) return Vector3.zero;

        Vector3 targetPos = currentRoom.GetRoomCentre();
        targetPos.z = transform.position.z;
        return targetPos;
    }

    public bool IsSwitching()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
