using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_Room : MonoBehaviour
{
    public int width;
    public int height;
    public int X;
    public int Y;
    public int totalEnemies = 0;
    private bool updatedDoors = false;
    public bool doorsUnlocked = true;
    public RG_Door leftDoor;
    public RG_Door rightDoor;
    public RG_Door topDoor;
    public RG_Door bottomDoor;
    public List<RG_Door> doors = new List<RG_Door>();
    public List<E_Spawn> spawners = new List<E_Spawn>();
    public List<E_EnemyController> enemies = new List<E_EnemyController>();

    public RG_Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    void Start()
    {
        if (RG_RoomController.instance == null)
        {
            Debug.Log("Escena equivocada!");
            return;
        }

        RG_Door[] _doors = GetComponentsInChildren<RG_Door>();
        E_Spawn[] _spawners = GetComponentsInChildren<E_Spawn>();

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

        foreach (E_Spawn spawn in _spawners)
            spawners.Add(spawn);

        RG_RoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if (name.Contains("Final") && !updatedDoors)
        {
            RemoveDoors();
            updatedDoors = true;
        }
    }

    public void RemoveDoors()
    {
        foreach(RG_Door door in doors)
        {
            Transform doorTransform = door.gameObject.transform;

            switch (door.doorDirection)
            {
                case RG_DoorDirection.right:
                    if (GetRight() == null)
                        doorTransform.GetChild(0).gameObject.SetActive(false);
                    else
                        doorTransform.GetChild(1).gameObject.SetActive(false);
                break;

                case RG_DoorDirection.left:
                    if (GetLeft() == null)
                        doorTransform.GetChild(0).gameObject.SetActive(false);
                    else
                        doorTransform.GetChild(1).gameObject.SetActive(false);
                break;

                case RG_DoorDirection.down:
                    if (GetBottom() == null)
                        doorTransform.GetChild(0).gameObject.SetActive(false);
                    else
                        doorTransform.GetChild(1).gameObject.SetActive(false);
                break;

                case RG_DoorDirection.top:
                    if (GetTop() == null)
                        doorTransform.GetChild(0).gameObject.SetActive(false);
                    else
                        doorTransform.GetChild(1).gameObject.SetActive(false);
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

    public void LockDoors(bool locked)
    {
        foreach (RG_Door door in doors)
        {
            Transform doorTransform = door.gameObject.transform;
            if (doorTransform.GetChild(0).gameObject.activeSelf)
            {
                if (!locked)
                    door.spriteRenderer.sprite = RG_RoomController.instance.doorSprites[0];
                else
                    door.spriteRenderer.sprite = RG_RoomController.instance.doorSprites[1];

                doorTransform.GetChild(1).gameObject.SetActive(locked);
            }
        }
    }

    public void EnemyKilled()
    {
        totalEnemies -= 1;
        if (totalEnemies == 0)
            LockDoors(false);
    }

    public void SpawnEnemies()
    {
        if (spawners.Count > 0)
        {
            LockDoors(true);
            foreach (E_Spawn spawn in spawners)
            {
                totalEnemies += spawn.enemiesAmount;
                spawn.Spawn();
            }
            spawners.Clear();
        }
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
