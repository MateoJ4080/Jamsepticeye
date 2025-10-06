using UnityEngine;

public class KeyInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX_KeyGrab);

        GameManager.Instance.r3_hasKeyToOutside = true;
        Destroy(gameObject);
    }
}
