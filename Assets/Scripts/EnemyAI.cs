using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.5f;

    [Header("Combat")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int attackDamage = 10;

    private NavMeshAgent agent;
    private float lastAttackTime;
    private float nextUpdateTime;
    private const float UPDATE_INTERVAL = 0.15f;

    private enum State { Idle, Chase, Attack }
    private State currentState = State.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = attackRange;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null || Time.time < nextUpdateTime) return;

        nextUpdateTime = Time.time + UPDATE_INTERVAL;

        float distance = Vector3.Distance(transform.position, player.position);
        UpdateState(distance);
    }

    void UpdateState(float distance)
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdle(distance);
                break;
            case State.Chase:
                HandleChase(distance);
                break;
            case State.Attack:
                HandleAttack(distance);
                break;
        }
    }

    void HandleIdle(float distance)
    {
        if (distance <= detectionRange)
            TransitionToChase();
    }

    void HandleChase(float distance)
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (distance <= attackRange)
            TransitionToAttack();
        else if (distance > detectionRange)
            TransitionToIdle();
    }

    void HandleAttack(float distance)
    {
        LookAtPlayer();

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }

        if (distance > attackRange)
            TransitionToChase();
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    void PerformAttack()
    {
        Debug.Log("Attacking");
    }

    void TransitionToIdle()
    {
        currentState = State.Idle;
        agent.isStopped = true;
    }

    void TransitionToChase()
    {
        currentState = State.Chase;
        agent.isStopped = false;
    }

    void TransitionToAttack()
    {
        currentState = State.Attack;
        agent.isStopped = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
