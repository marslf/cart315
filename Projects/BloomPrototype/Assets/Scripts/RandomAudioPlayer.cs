using UnityEngine;
using System.Collections;

public class SmoothRandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public float fadeDuration = 2f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        StartCoroutine(PlayRandomClipsSmooth());
    }

    IEnumerator PlayRandomClipsSmooth()
    {
        while (true)
        {
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            
            if (audioSource.isPlaying)
            {
                yield return StartCoroutine(FadeAudio(0f));
            }
            
            audioSource.clip = clip;
            audioSource.Play();
            
            yield return StartCoroutine(FadeAudio(1f));
            yield return new WaitForSeconds(clip.length - fadeDuration);
        }
    }

    IEnumerator FadeAudio(float targetVolume)
    {
        float startVolume = audioSource.volume;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}