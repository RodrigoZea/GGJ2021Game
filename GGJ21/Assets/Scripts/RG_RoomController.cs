using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string Name;
    public int X, Y;
    public RoomInfo(string name, int x, int y)
    {
        Name = name;
        X = x;
        Y = y;
    }
}

public class RG_RoomController : MonoBehaviour
{
    public static RG_RoomController instance;
    public List<RG_Room> loadedRooms = new List<RG_Room>();
    RoomInfo currentLoadRoomData;
    RG_Room currentRoom;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    bool isLoadingRoom;
    bool updatedRooms = false;
    bool spawnedFinalRoom = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // LoadRoom("Room1", 0, 0);
        // LoadRoom("Room1", 1, 0);
        // LoadRoom("Room1", 0, 1);
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom) return;

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedFinalRoom)
                StartCoroutine(SpawnFinalRoom());

            else if (spawnedFinalRoom && !updatedRooms)
            {
                foreach (RG_Room room in loadedRooms)
                    room.RemoveDoors();

                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnFinalRoom()
    {
        spawnedFinalRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            RG_Room finalRoom = loadedRooms[loadedRooms.Count - 1];
            RG_Room temp = new RG_Room(finalRoom.X, finalRoom.Y);

            Destroy(finalRoom.gameObject);

            var roomToRemove = loadedRooms.Single(r => r.X == temp.X && r.Y == temp.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Final", temp.X, temp.Y);
        }
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (!RoomExists(x, y))
        {
            RoomInfo roomInfo = new RoomInfo(name, x, y);
            loadRoomQueue.Enqueue(roomInfo);
        }
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = "RG_" + info.Name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(
            roomName,
            LoadSceneMode.Additive
        );

        while(loadRoom.isDone == false)
            yield return null;
    }

    public void RegisterRoom(RG_Room room)
    {
        // Validate if room exists before creating one
        if (!RoomExists(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.width,
                currentLoadRoomData.Y * room.height
            );

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentLoadRoomData.Name + "_" + room.X + ", " + room.Y;
            room.transform.parent = transform;

            // Finished loading
            isLoadingRoom = false;

            // If theres no room assigned to the camera, assign the one thats registering
            if (loadedRooms.Count == 0) RG_CameraController.instance.currentRoom = room;

            // Add it to the list
            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }

    }

    public bool RoomExists(int x, int y)
    {
        return loadedRooms.Find(room => room.X == x && room.Y == y) != null;
    }

    public RG_Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(room => room.X == x && room.Y == y);
    }

    public void OnPlayerEnterRoom(RG_Room room)
    {
        // We set so that the current room asigned to the camera changes
        RG_CameraController.instance.currentRoom = room;
        currentRoom = room;
        currentRoom.SpawnEnemies();
    }
}
