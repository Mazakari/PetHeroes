using UnityEngine;

public class LookDirectionRotator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    private float _previousXPosition = 0;
    private bool _lookLeft = false;

    private void Start() => 
        InitLookDirection();
    
    private void Update()
    {
        DetermineXDirection();
        SetSpriteXDirection();
    }

    private void InitLookDirection()
    {
        _lookLeft = false;
        _previousXPosition = 0;
        _playerSpriteRenderer.flipX = _lookLeft;
    }

    private void DetermineXDirection()
    {
        if (transform.position.x >= _previousXPosition)
        {
            _lookLeft = false;
        }

        if (transform.position.x <= _previousXPosition)
        {
            _lookLeft = true;
        }

        _previousXPosition = transform.position.x;
    }

    private void SetSpriteXDirection() =>
        _playerSpriteRenderer.flipX = _lookLeft;

}
