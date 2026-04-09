using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoopingAudio : MonoBehaviour
{
    public AudioClip clip; 

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = true; 
        audioSource.playOnAwake = true; 
        audioSource.Play();
    }
}
