using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _enemyBody;
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [SerializeField] private float _lerpSpeed = 0.1f;

    [SerializeField] private AnimationCurve _animationCurve;

    private float _lerpValue = 0f;
    private float _direction = 1f;
    private bool _moveRight = true;

    void Start()
    {
        _spriteRenderer.flipX = _moveRight;
        _enemyBody.position = _pointA.position;
    }

    void Update()
    {
        LerpPosition();
        _lerpValue += _direction * _lerpSpeed * Time.deltaTime;

        ChangeLerpDirection();
    }

    private void ChangeLerpDirection()
    {
        if (_lerpValue >= 1f && _moveRight == true)
        {
            _direction = -1;
            _moveRight = false;

            _spriteRenderer.flipX = _moveRight;

        }
        else if (_lerpValue <= 0 && _moveRight == false)
        {
            _direction = 1;
            _moveRight = true;

            _spriteRenderer.flipX = _moveRight;
        }

        
    }

    private void LerpPosition() => 
        _enemyBody.position = Vector3.Lerp(_pointA.position, _pointB.position, _animationCurve.Evaluate(_lerpValue));
}
