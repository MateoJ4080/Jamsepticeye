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
    public AudioClip SFX_Swing;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
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

    public IEnumerator PlaySoundDelayed(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        sfxSource.PlayOneShot(clip);
    }
}
