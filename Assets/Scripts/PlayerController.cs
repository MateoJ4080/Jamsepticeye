using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothTime = 0.1f;

    private CharacterController characterController;
    private InputSystem_Actions controls;
    private Vector2 moveInput;
    private Vector2 currentInput;
    private Vector2 inputVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        currentInput = Vector2.SmoothDamp(currentInput, moveInput, ref inputVelocity, smoothTime);
        Vector3 move = new Vector3(currentInput.x, 0, currentInput.y);
        if (move.magnitude > 1f) move.Normalize();
        characterController.Move(move * speed * Time.deltaTime);
    }
}
