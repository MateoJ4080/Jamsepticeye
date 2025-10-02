using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play() => SceneManager.LoadScene(1);

#if UNITY_EDITOR
    public void Quit() => EditorApplication.ExitPlaymode();
#else
    public void Quit() => Application.Quit();
#endif
}
