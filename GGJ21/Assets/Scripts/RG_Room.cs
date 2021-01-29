using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_Room : MonoBehaviour
{
    public int width;
    public int height;
    public int X;
    public int Y;
    void Start()
    {
        if (RG_RoomController.instance == null)
        {
            Debug.Log("Escena equivocada!");
            return;
        }

        RG_RoomController.instance.RegisterRoom(this);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, Y * height);
    }
}
