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
            Instance = this;
            return;
        }
        Instance = this;

        if (SceneManager.GetActiveScene().name == "Level") StartCoroutine(FadeManager.Instance.FadeIn());
    }

    public IEnumerator LoadSceneFade(string sceneName)
    {
        yield return FadeManager.Instance.FadeOut();

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
            yield return null;

        op.allowSceneActivation = true;

        while (!op.isDone)
            yield return null;

        yield return FadeManager.Instance.FadeIn();
    }
}
