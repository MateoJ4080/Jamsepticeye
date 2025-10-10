using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour
{
    [Header("Title Image")]
    [SerializeField] private RawImage titleImage;
    [SerializeField] private float fadeInSpeed = 1f;
    [SerializeField] private float fadeOutSpeed = 1f;

    [Header("Credits")]
    [SerializeField] private RectTransform creditsObj;
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioSource music;

    [Header("Settings")]
    [SerializeField] private float scrollDuration = 10f;

    void Start()
    {
        titleImage.color = new Color(1, 1, 1, 0);
        StartCoroutine(PlayCredits());
    }

    IEnumerator PlayCredits()
    {
        yield return StartCoroutine(FadeImage(titleImage, 0f, 1f, fadeInSpeed));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeImage(titleImage, 1f, 0f, fadeOutSpeed));

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        // System supossed to work with the anchor on the top of creditsObj
        Vector3 pos = creditsObj.anchoredPosition;
        pos.y = canvasRect.rect.center.y - canvasRect.rect.height / 2;
        creditsObj.anchoredPosition = pos;

        float startY = pos.y;
        float endY = canvasRect.rect.center.y + canvasRect.rect.height / 2 + creditsObj.rect.height;

        // Debug.Log($"Canvas center: {canvasRect.position}, Credits anchoredPos: {creditsObj.anchoredPosition}");

        // Debug.Log($"Canvas height: {canvasRect.rect.height}");
        // Debug.Log($"Credits height: {creditsObj.rect.height}");
        // Debug.Log($"StartY: {startY}, EndY: {endY}");

        float elapsed = 0f;
        while (elapsed < scrollDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / scrollDuration);
            pos.y = Mathf.Lerp(startY, endY, t);
            creditsObj.anchoredPosition = pos;
            yield return null;
        }

        SceneManager.LoadScene(0);
    }

    IEnumerator FadeImage(RawImage image, float from, float to, float speed)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            float alpha = Mathf.Lerp(from, to, t);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }
    }
}
