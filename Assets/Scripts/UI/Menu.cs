using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource menuSource;

    void Start() => menuSource.Play();

    public void Play()
    {
        StartCoroutine(SceneLoader.Instance.LoadSceneFade("Level"));
        menuSource.Stop();
    }

#if UNITY_EDITOR
    public void Quit() => EditorApplication.ExitPlaymode();
#else
    public void Quit() => Application.Quit();
#endif
}
