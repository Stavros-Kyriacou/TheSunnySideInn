using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private bool playParticlesOnAwake;
    void Awake()
    {
        if (!playParticlesOnAwake) return;

        particles.Play();
    }
    public void PlayParticles()
    {
        particles.Play();
    }
    public void StopParticles()
    {
        particles.Stop();
    }
}