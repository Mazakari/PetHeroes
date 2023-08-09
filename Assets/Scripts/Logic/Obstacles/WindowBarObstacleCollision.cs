using UnityEngine;

public class WindowBarObstacleCollision : MonoBehaviour, IObstacleCollision
{
    [Header("Collision Settings")]
    [SerializeField] private int _playerLayer;

    [Space(10)]
    [Header("Components References")]
    [SerializeField] private ItemSound _sound;
    [SerializeField] private ObstacleHealth _obstacleHealth;

    private void OnCollisionEnter2D(Collision2D collision) =>
        Collision(collision);

    public void Collision(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            _obstacleHealth.DecreaseHealth();

            PlayCollisionSound();
        }
    }

    private void PlayCollisionSound() => 
        _sound.Play();
}
