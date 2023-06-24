using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _playerJumpUpSprite;
    [SerializeField] private Sprite _playerFallDownSprite;
    [SerializeField] private Sprite _playerHangingSprite;
    [SerializeField] private Sprite _playerWinSprite;

    public void SetJumpingUpSprite()
    {
        if (_spriteRenderer && _playerJumpUpSprite)
        {
            _spriteRenderer.sprite= _playerJumpUpSprite;
        }
    }

    public void SetFallingDownSprite()
    {
        if (_spriteRenderer && _playerFallDownSprite)
        {
            _spriteRenderer.sprite = _playerFallDownSprite;
        }
    }

    public void SetWinSprite()
    {
        if (_spriteRenderer && _playerWinSprite)
        {
            _spriteRenderer.sprite = _playerWinSprite;
        }
    }
}
