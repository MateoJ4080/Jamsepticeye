using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;
    public string Hovering = "Hover";
    public string Idle = "Idle";

    public string SceneName;

    public PauseMenu PM;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering");
        animator.SetTrigger(Hovering);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger(Idle);
    }

    public void Unpause()
    {
        PM.PauseMenuButtons.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
