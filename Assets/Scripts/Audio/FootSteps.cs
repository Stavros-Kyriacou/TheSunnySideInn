using UnityEngine;

namespace Audio
{
    public class FootSteps : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void Step()
        {
            AudioClip clip = GetRandomClip();
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }

        private AudioClip GetRandomClip()
        {
            return clips[UnityEngine.Random.Range(0, clips.Length)];
        }
    }
}