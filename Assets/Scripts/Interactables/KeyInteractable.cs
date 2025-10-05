using UnityEngine;

public class KeyInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.r1_hasOutsideKey = true;
        Destroy(gameObject);
    }
}
