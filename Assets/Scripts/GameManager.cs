using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    // r1-r3 prefix stands for the rooms
    public bool r1_boxMoved = false;
    public bool r1_hasOutsideKey;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
