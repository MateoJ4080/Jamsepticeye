using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseMenuButtons;
    private InputSystem_Actions controls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PauseMenuButtons.SetActive(false);
        controls = new InputSystem_Actions();
    }


    private void OnEnable()
    {
        controls.UI.PauseMenu.performed += PauseOn;
        controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.UI.PauseMenu.performed -= PauseOn;
        controls.UI.Disable();
    }

    private void PauseOn(InputAction.CallbackContext context)
    {
        Time.timeScale = 0f;
        PauseMenuButtons.SetActive(true);
    }
}
