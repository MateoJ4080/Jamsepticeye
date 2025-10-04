using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public bool r1_boxMoved = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
