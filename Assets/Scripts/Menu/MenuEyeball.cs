using UnityEngine;
using UnityEngine.InputSystem;

public class MenuEyeball : MonoBehaviour
{
    void Update()
    {
        var rawCursorPos = Mouse.current.position;
        Vector3 cursorPos = rawCursorPos.ReadValue();
        cursorPos.z = 0.5f;
        var cursorPosWorld = Camera.main.ScreenToWorldPoint(cursorPos);
    }
}
