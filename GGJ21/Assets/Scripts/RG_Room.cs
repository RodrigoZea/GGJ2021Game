using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_Room : MonoBehaviour
{
    public int width;
    public int height;
    public int X;
    public int Y;
    public RG_Door leftDoor;
    public RG_Door rightDoor;
    public RG_Door topDoor;
    public RG_Door bottomDoor;
    public List<RG_Door> doors = new List<RG_Door>();
    void Start()
    {
        if (RG_RoomController.instance == null)
        {
            Debug.Log("Escena equivocada!");
            return;
        }

        RG_Door[] _doors = GetComponentsInChildren<RG_Door>();

        foreach (RG_Door d in _doors)
        {
            doors.Add(d);
            switch (d.doorDirection)
            {
                case RG_DoorDirection.right:
                    rightDoor = d;
                break;

                case RG_DoorDirection.left:
                    leftDoor = d;
                break;

                case RG_DoorDirection.down:
                    bottomDoor = d;
                break;

                case RG_DoorDirection.top:
                    topDoor = d;
                break;
            }
        }

        RG_RoomController.instance.RegisterRoom(this);
    }

    public void RemoveDoors()
    {
        foreach(RG_Door door in doors)
        {
            switch (door.doorDirection)
            {
                case RG_DoorDirection.right:
                    if (GetRight() == null)
                        door.gameObject.SetActive(false);
                break;

                case RG_DoorDirection.left:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                break;

                case RG_DoorDirection.down:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                break;

                case RG_DoorDirection.top:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                break;   
            }
        }
    }

    public RG_Room GetRight()
    {
        if (RG_RoomController.instance.RoomExists(X + 1, Y))
            return RG_RoomController.instance.FindRoom(X + 1, Y);
        return null;
    }

    public RG_Room GetLeft()
    {
        if (RG_RoomController.instance.RoomExists(X - 1, Y))
            return RG_RoomController.instance.FindRoom(X - 1, Y);
        return null;
    }

    public RG_Room GetTop()
    {
        if (RG_RoomController.instance.RoomExists(X, Y + 1))
            return RG_RoomController.instance.FindRoom(X, Y + 1);
        return null;
    }

    public RG_Room GetBottom()
    {
        if (RG_RoomController.instance.RoomExists(X, Y - 1))
            return RG_RoomController.instance.FindRoom(X, Y - 1);
        return null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, Y * height);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player")
            RG_RoomController.instance.OnPlayerEnterRoom(this);
    }
}
