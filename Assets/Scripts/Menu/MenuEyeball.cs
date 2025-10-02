using UnityEngine;
using UnityEngine.InputSystem;

public class MenuEyeball : MonoBehaviour
{
    public Vector3 offset;
    private Vector3 forward;

    void Start() => forward = transform.localEulerAngles;

    void Update()
    {
        var rawCursorPos = Mouse.current.position;
        Vector3 cursorPos = rawCursorPos.ReadValue();
        cursorPos.z = 0.5f;
        var cursorPosWorld = Camera.main.ScreenToWorldPoint(cursorPos);
        transform.LookAt(cursorPosWorld);
        transform.Rotate(forward - offset);
    }
}
