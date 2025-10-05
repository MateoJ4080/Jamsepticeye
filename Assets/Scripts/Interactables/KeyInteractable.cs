using UnityEngine;

public class KeyInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.r3_hasKeyToOutside = true;
        Destroy(gameObject);
    }
}
