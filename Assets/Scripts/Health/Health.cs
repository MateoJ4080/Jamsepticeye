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

        OnHealthChanged?.Invoke();

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        if (TryGetComponent<EnemyAI>(out _)) Destroy(gameObject);

        if (gameObject.GetComponent<PlayerController>() != null)
        {
            // animator.SetTrigger("Dead");
            // Leave body on the floor
            StartCoroutine(AudioManager.Instance.PlaySFXAndWait(AudioManager.Instance.SFX_PlayerDeath));
            SceneManager.LoadScene("Level");
        }
    }

    private IEnumerator HitFlash()
    {
        if (meshRenderer == null) yield break;

        originalColor = meshRenderer.material.color;

        meshRenderer.material.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        meshRenderer.material.color = originalColor;
    }
}
