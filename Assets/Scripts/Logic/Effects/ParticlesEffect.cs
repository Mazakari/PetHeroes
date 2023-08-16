using UnityEngine;

public class ParticlesEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    public void Play()
    {
        if (_particles != null)
        {
            _particles.Play();
        }
    }
}
