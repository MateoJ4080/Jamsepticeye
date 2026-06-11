using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public IEnumerator LoadSceneFade(string sceneName)
    {
        yield return FadeManager.Instance.FadeOut();
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return FadeManager.Instance.FadeIn();
    }
}
