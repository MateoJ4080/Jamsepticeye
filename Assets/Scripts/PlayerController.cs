using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothTime = 0.1f;
    private CharacterController characterController;
    private InputSystem_Actions controls;

    [Header("Combat")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask enemyLayer;

    private Vector2 moveInput;
    private Vector2 currentInput;
    private Vector2 inputVelocity;
    private Quaternion lastRotation;
    private bool canAttack;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        controls = new InputSystem_Actions();

        lastRotation = transform.rotation;
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
        controls.Player.Attack.performed += OnAttack;
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
        controls.Player.Attack.performed -= OnAttack;

        controls.Player.Disable();
    }

    private void Update()
    {
        currentInput = Vector2.SmoothDamp(currentInput, moveInput, ref inputVelocity, smoothTime);
        Vector3 move = new(currentInput.x, 0, currentInput.y);
        if (move.magnitude > 1f) move.Normalize();

        characterController.Move(speed * Time.deltaTime * move);

        if (move.x != 0)
        {
            Vector3 lookDirection = new(move.x, 0, 0);
            if (lookDirection != Vector3.zero)
            {
                lastRotation = Quaternion.LookRotation(lookDirection);
            }
        }

        transform.rotation = lastRotation;
    }

    private void Attack()
    {
        // animator.SetTrigger("Attack");

        Vector3 center = transform.position + transform.forward * attackRange / 2f;
        float radius = attackRange / 2f;

        Collider[] hits = Physics.OverlapSphere(center, radius, enemyLayer);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Health>(out var enemyHealth))
                enemyHealth.TakeDamage(attackDamage);
        }

        // canAttack = false;
    }
    public void OnMove(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();
    public void OnAttack(InputAction.CallbackContext context) => Attack();

    // Call in one frame of the animation clip - ¡Make sure canAttack is set back to false in Attack()!
    public void OnAttackEnd() => canAttack = true;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position + transform.forward * attackRange / 2f;
        float radius = attackRange / 2f;

        Gizmos.DrawWireSphere(center, radius);
    }
}
