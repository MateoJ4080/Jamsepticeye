using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image[] heartParts; // 0: bottom - 1: top left - 2: top right

    void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealthUI;
    }

    void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealthUI;
    }

    public void UpdateHealthUI()
    {
        for (int i = 0; i < heartParts.Length; i++)
        {
            heartParts[i].enabled = i < playerHealth.CurrentHealth;
        }
    }
}
