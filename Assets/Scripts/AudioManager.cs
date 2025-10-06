using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

    [Header("Audio Sources")]
    public AudioSource sfxSource;

    [Header("Sound Effects")]
    public AudioClip SFX_DoorUnlock;
    public AudioClip SFX_KeyGrab;
    public AudioClip SFX_PlayerDeath;
    public AudioClip SFX_PlayerTakeDmg;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        Debug.Log($"PlaySFX with {clip}");
        sfxSource.PlayOneShot(clip);
    }


    public IEnumerator PlaySFXAndWait(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
    }
}
