using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime = 0.3f;

    private Vector3 velocity;
    private Vector3 targetPos;
    private bool isMoving = false;
    private Room targetRoom;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        RoomManager.Instance.OnRoomChanged += HandleRoomChanged;
    }

    public void HandleRoomChanged(Room room)
    {
        targetRoom = room;
    }

    void Update()
    {
        if (targetRoom != null)
        {
            BoxCollider roomCollider = targetRoom.GetComponent<BoxCollider>();
            Collider playerCollider = player.GetComponent<Collider>();

            if (roomCollider.bounds.Contains(playerCollider.bounds.min) &&
                roomCollider.bounds.Contains(playerCollider.bounds.max))
            {
                float roomPosX = targetRoom.transform.position.x;
                targetPos = new Vector3(roomPosX, transform.position.y, transform.position.z);
                isMoving = true;
                targetRoom = null;
            }
        }

        if (isMoving)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                transform.position = targetPos;
                isMoving = false;
            }
        }
    }

    public void TeleportToX(float value)
    {
        transform.position = new(value, transform.position.y, transform.position.z);
    }
}
