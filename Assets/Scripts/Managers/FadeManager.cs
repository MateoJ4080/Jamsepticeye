using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private float fadeDuration;
    [SerializeField] private CanvasGroup _canvasGroup;

    public static FadeManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = elapsedTime / fadeDuration;
            yield return null;
        }
        _canvasGroup.alpha = 1;
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = 1f - elapsedTime / fadeDuration;
            yield return null;
        }
        _canvasGroup.alpha = 0;
    }
}
