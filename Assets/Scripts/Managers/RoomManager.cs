using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; set; }

    [SerializeField] private Room[] rooms;

    private Room currentRoom;
    public Room CurrentRoom
    {
        get => currentRoom;
        set => currentRoom = value;
    }
    public event Action<Room> OnRoomChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ChangeRoom(Room room)
    {
        if (CurrentRoom != room)
        {
            CurrentRoom = room;
            OnRoomChanged?.Invoke(room);
        }
    }
}