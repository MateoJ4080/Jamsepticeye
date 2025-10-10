using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public InputSystem_Actions Actions { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Actions = new InputSystem_Actions();
        Actions.Enable();
    }

    void OnDisable()
    {
        Actions.Disable();
    }
}