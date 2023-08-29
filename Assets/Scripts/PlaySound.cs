using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    public void Play()
    {
        AudioManager.Instance.PlaySound(clip);
    }
    public void PlayWithDelay(float delay)
    {
        AudioManager.Instance.PlaySoundWithDelay(clip, delay);
    }

}
