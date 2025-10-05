using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    // r1-r3 prefix stands for the rooms
    public bool r1_boxMoved;
    public bool r1_hasKeyToHallway;
    public bool r3_hasKeyToOutside;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
