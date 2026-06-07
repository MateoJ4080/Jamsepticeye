using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Renderer))]
public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public event Action OnHealthChanged;

    [SerializeField] private AudioClip takeDamageSFX;

    [Header("Hit Flash")]
    [SerializeField] private Color hitColor = Color.white;
    [SerializeField] private float hitDuration = 0.1f;
    private Renderer meshRenderer;
    private Color originalColor;

    private void Awake()
    {
        currentHealth = maxHealth;
        meshRenderer = GetComponent<Renderer>();
    }

    public void TakeDamage(int amount)
    {
        AudioManager.Instance.PlaySFX(takeDamageSFX);
        StartCoroutine(HitFlash());
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth <= 0) Die();
        OnHealthChanged?.Invoke();
        Debug.Log($"{gameObject.name} damaged, HP: {currentHealth}");
    }

    private void Die()
    {
        if (gameObject.TryGetComponent(out EnemyAI enemy))
        {
            if (enemy.dropsKey)
            {
                Collider enemyCollider = GetComponent<Collider>();
                float dropPosY = enemyCollider.bounds.center.y;

                Vector3 spawnPos = new(transform.position.x, dropPosY, transform.position.z);
                Instantiate(enemy.keyPrefab, spawnPos, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        if (gameObject.GetComponent<PlayerController>() != null)
        {
            // animator.SetTrigger("Dead");
            // Leave body on the floor
            StartCoroutine(AudioManager.Instance.PlaySFXAndWait(AudioManager.Instance.SFX_PlayerDeath));
            SceneManager.LoadScene("Level");
        }
    }

    IEnumerator HitFlash()
    {
        if (meshRenderer == null) yield break;

        originalColor = meshRenderer.material.color;

        meshRenderer.material.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        meshRenderer.material.color = originalColor;
    }
}
