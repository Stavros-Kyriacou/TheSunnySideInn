using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    public void PlaySoundWithDelay(AudioClip clip, float delay)
    {
        StartCoroutine(PlaySoundWithDelayRoutine(clip, delay));
    }
    private IEnumerator PlaySoundWithDelayRoutine(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);

        effectsSource.PlayOneShot(clip);
    }

}
