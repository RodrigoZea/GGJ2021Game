using System.Collections;
using System.Collections.Generic;
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
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    bool isLoadingRoom;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadRoom("Room1", 0, 0);
        LoadRoom("Room1", 1, 0);
        LoadRoom("Room1", 0, 1);
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom) return;
        if (loadRoomQueue.Count == 0) return;

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
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
        {
            yield return null;
        }
    }

    public void RegisterRoom(RG_Room room)
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

        // Add it to the list
        loadedRooms.Add(room);
    }

    public bool RoomExists(int x, int y)
    {
        return loadedRooms.Find(room => room.X == x && room.Y == y) != null;
    }
}
