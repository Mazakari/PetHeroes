using UnityEngine;

public class ObstacleHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private SwitchHealthSprites _switchHealthSprite;

    private void OnEnable() =>
      EnableCollider();

    public void DecreaseHealth()
    {
        if (_health > 0)
        {
            _health--;
            _switchHealthSprite.SwitchHealthSprite();
        }
        else
        {
            DisableCollider();
            _switchHealthSprite.DisableSpriteRenderer();
        }
    }

    private void EnableCollider() =>
      _collider.enabled = true;
    private void DisableCollider() =>
       _collider.enabled = false;
}
