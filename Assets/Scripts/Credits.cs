using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public RectTransform creditsObj;
    public AudioSource music;

    [Header("Settings")]
    public float endTimeMargin = 0;

    void Update()
    {
        float t = (1f / (music.clip.length - endTimeMargin)) * music.time;
        float position = Mathf.Lerp(0, creditsObj.sizeDelta.y + 640, t);
        creditsObj.anchoredPosition = Vector2.up * position;
        if (!music.isPlaying) { SceneManager.LoadScene(0); }
    }
}
