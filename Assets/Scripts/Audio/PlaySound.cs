using UnityEngine;
using Managers;

namespace Audio
{
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
}