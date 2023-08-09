using UnityEngine;

public class FlyingObstacleCollision : MonoBehaviour, IObstacleCollision
{
    [Header("Collision Settings")]
    [SerializeField] private int _playerLayer;

    [Space(10)]
    [Header("Components References")]
    [SerializeField] private ItemSound _sound;
    [SerializeField] private CollisionEffect _effect;

    private void OnCollisionEnter2D(Collision2D collision) =>
        Collision(collision);

    public void Collision(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            PlayCollisionSound();
            PlayCollisionParticles();
        }
    }

    private void PlayCollisionSound() =>
        _sound.Play();

    private void PlayCollisionParticles() =>
       _effect.Play();
}
