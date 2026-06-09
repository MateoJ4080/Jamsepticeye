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
    [SerializeField] private float knockbackDistance = 1;
    [SerializeField] private float knockbackDuration = 1;
    [SerializeField] private LayerMask enemyLayer;
    private bool isAttacking;

    [SerializeField] Animator animator;
    private Vector2 moveInput;
    private Vector2 currentInput;
    private Vector2 inputVelocity;
    private Quaternion lastRotation;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

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

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isAttacking) return;

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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat(SpeedHash, moveInput.magnitude);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        TryAttack();
    }

    // Triggered by animation event
    public void OnAttackEnd() => isAttacking = false;

    private void TryAttack()
    {
        isAttacking = true;
        currentInput = Vector2.zero;
        inputVelocity = Vector2.zero;

        animator.SetTrigger(AttackHash);
    }

    // Triggered by animation event
    public void DealDamage()
    {
        Vector3 center = transform.position + transform.forward * attackRange / 2f;
        float radius = attackRange / 2f;

        Collider[] hits = Physics.OverlapSphere(center, radius, enemyLayer);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Health>(out var enemyHealth))
                enemyHealth.TakeDamage(attackDamage);
            if (hit.TryGetComponent<EnemyAI>(out var enemy))
            {
                var enemyPos = hit.transform.position;
                var playerPos = transform.position;

                var direction = (enemyPos - new Vector3(playerPos.x, enemyPos.y, playerPos.z)).normalized;
                StartCoroutine(enemy.Knockback(direction, knockbackDistance, knockbackDuration));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position + transform.forward * attackRange / 2f;
        float radius = attackRange / 2f;

        Gizmos.DrawWireSphere(center, radius);
    }

    public void TeleportToX(float value)
    {
        transform.position = new(value, transform.position.y, transform.position.z);
    }
}
