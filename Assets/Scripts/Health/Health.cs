using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public event Action OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        Debug.Log($"{gameObject.name} DAMAGED. HP: {currentHealth}");

        if (currentHealth <= 0) Die();
        OnHealthChanged?.Invoke();
    }

    private void Die()
    {
        if (gameObject.GetComponent<EnemyAI>() != null)
        {
            Destroy(gameObject);
        }

        if (gameObject.GetComponent<PlayerController>() != null)
        {
            // animator.SetTrigger("Dead");
            // Leave body in the floor
            Debug.Log("[Health] PLAYER DIED");
        }
    }
}
