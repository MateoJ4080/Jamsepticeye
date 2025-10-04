using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float duration;

    public void Interact()
    {
        // if (SanitySystem.Instance.currentState == SanitySystem.SanityState.Medium)
        if (!GameManager.Instance.r1_boxMoved) StartCoroutine(MoveTo(targetPos, duration));
    }

    IEnumerator MoveTo(Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
        GameManager.Instance.r1_boxMoved = true;
    }
}
