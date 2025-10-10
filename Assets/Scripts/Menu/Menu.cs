using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource menuSource;

    void Start() => menuSource.Play();

    public void Play()
    {
        SceneManager.LoadScene(2);
        menuSource.Stop();
    }

#if UNITY_EDITOR
    public void Quit() => EditorApplication.ExitPlaymode();
#else
    public void Quit() => Application.Quit();
#endif
}
