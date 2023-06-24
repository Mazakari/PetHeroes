using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    [SerializeField] private int _playerLayer;
    [SerializeField] private int _maxPlatformHealth = 1;
    private int _curPlatformHealth = 1;

    [SerializeField] private float _bounceForce = 1f;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private GameObject _platform;

    private Rigidbody2D _playerRigidbody2D;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    private void Start() => 
        InitPlatformHealth();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == _playerLayer)
        {
            if (_playerRigidbody2D == null)
            {
                _playerRigidbody2D = collision.collider.gameObject.GetComponent<Rigidbody2D>();
            }

            if (PlayerAbovePlatform(collision.gameObject.transform))
            {
                BounceUp(_playerRigidbody2D);
                UpdatePlatformHealth();

                PlayItemSound();

                if (_curPlatformHealth == 0)
                {
                    TurnOffBouncer();
                }
            }
        }
    }

    private void BounceUp(Rigidbody2D rb) => 
        rb.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);

    private void UpdatePlatformHealth()
    {
        _curPlatformHealth -= 1;
        _curPlatformHealth = Mathf.Clamp(_curPlatformHealth, 0, _maxPlatformHealth);
    }

    private void TurnOffBouncer()
    {
        _platform.SetActive(false);
        _collider.enabled= false;
    }

    private void InitPlatformHealth() => 
        _curPlatformHealth = _maxPlatformHealth;

    private bool PlayerAbovePlatform(Transform player)
    {
        float playerYPosition = player.position.y;
        float platformYPosition = transform.position.y;

        return playerYPosition > platformYPosition;
    }

    private void PlayItemSound()
    {
        if (_itemSound)
        {
            _itemSound.Play();
        }
    }
}
