using UnityEngine;
using System;

public class SanitySystem : MonoBehaviour
{
    public static SanitySystem Instance { get; private set; }

    [SerializeField] private int startingSanity = 100;
    [SerializeField] private int highThreshold = 50;
    [SerializeField] private int lowThreshold = 20;

    private int sanity;
    public int Sanity => sanity;

    public enum SanityState { High, Medium, Low }
    public SanityState currentState;

    public event Action<SanityState, int> OnSanityChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        sanity = startingSanity;
        UpdateState();
    }

    public void IncreaseSanity(int amount)
    {
        if (amount <= 0) return;

        sanity = Mathf.Clamp(sanity + amount, 0, 100);
        UpdateState();
    }

    public void DecreaseSanity(int amount)
    {
        if (amount <= 0) return;

        sanity = Mathf.Clamp(sanity - amount, 0, 100);
        UpdateState();
    }

    private void UpdateState()
    {
        SanityState previousState = currentState;

        if (sanity > highThreshold) currentState = SanityState.High;
        else if (sanity > lowThreshold) currentState = SanityState.Medium;
        else currentState = SanityState.Low;

        OnSanityChanged?.Invoke(currentState, sanity);
    }
}
