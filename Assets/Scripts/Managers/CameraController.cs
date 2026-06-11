using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;

    public static CameraController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (cam == null) cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        var playerPos = player.transform.position;
        var camPos = cam.transform.position;

        cam.transform.position = new(playerPos.x, camPos.y, camPos.z);
    }
}
