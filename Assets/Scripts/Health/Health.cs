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
    private Renderer[] renderers;
    private Color[] originalColors;

    private void Awake()
    {
        currentHealth = maxHealth;

        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
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
        if (TryGetComponent<EnemyAI>(out var enemy)) enemy.Die();

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
        if (renderers.Length == 0) yield break;

        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
            renderers[i].material.color = hitColor;
        }

        yield return new WaitForSeconds(hitDuration);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = originalColors[i];
        }
    }
}
