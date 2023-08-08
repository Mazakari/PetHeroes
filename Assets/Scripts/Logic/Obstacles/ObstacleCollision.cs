using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] private int _health;
    [SerializeField] private int _playerLayer;

    [Space(10)]
    [Header("Components References")]
    [SerializeField] private ItemSound _sound;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private SwitchHealthSprites _switch;

    private void OnEnable() => 
        EnableCollider();

    private void OnCollisionEnter2D(Collision2D collision) => 
        DecreaseHPandPlaySound(collision);
    private void EnableCollider() =>
       _collider.enabled = true;

    private void DecreaseHPandPlaySound(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            if (_health > 0)
            {
                _health--;
                _switch.SwitchHealthSprite();
            }
            else
            {
                DisableCollider();
                _switch.DisableSpriteRenderer();
            }
        }

        PlayCollisionSound();
    }

    private void DisableCollider() => 
        _collider.enabled = false;

    private void PlayCollisionSound() => 
        _sound.Play();
}
