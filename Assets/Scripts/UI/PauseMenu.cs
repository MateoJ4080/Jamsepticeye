using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuButtons;
    private InputAction pauseAction;

    void Awake()
    {
        PauseMenuButtons.SetActive(false);
    }

    void Start()
    {
        pauseAction = InputManager.Instance.Actions.UI.PauseMenu;
    }

    void OnDisable()
    {
        pauseAction.performed -= PauseOn;
    }

    void PauseOn(InputAction.CallbackContext context)
    {
        Time.timeScale = 0f;
        PauseMenuButtons.SetActive(true);
    }
}
