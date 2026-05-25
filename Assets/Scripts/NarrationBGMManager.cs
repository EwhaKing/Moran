using UnityEngine;

public class NarrationBGMManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip firstNarrationMusic;
    public AudioClip nextMusic;

    public float narrationDuration = 10f;

    void Start()
    {
        audioSource.clip = firstNarrationMusic;
        audioSource.loop = false;
        audioSource.Play();

        Invoke(nameof(PlayNextMusic), narrationDuration);
    }

    void PlayNextMusic()
    {
        audioSource.Stop();

        audioSource.clip = nextMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}