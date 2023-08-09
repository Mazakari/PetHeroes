using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _landParticles;

    public void Play()
    {
        if (_landParticles != null)
        {
            _landParticles.Play();
        }
    }
}
