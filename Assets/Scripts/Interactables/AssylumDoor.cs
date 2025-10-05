using UnityEngine;
using UnityEngine.SceneManagement;

public class AssylumDoor : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (GameManager.Instance.r3_hasKeyToOutside) SceneManager.LoadScene("Credits");
        // else AudioManager.Instance.PlaySFX(sfxTryingToOpenLocked)
    }
}
