using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private Transform _enemyBody;

    [SerializeField] private float _angle = 90f;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;

    [SerializeField] private float _speed = 1f;
    private float _direction = 1f;

    [SerializeField] private AnimationCurve _animationCurve;
    private float _lerpValue;

    private void Start() =>
        SetMinAndMaxRotation();

    void Update() => 
        PingPongRotation();

    private void PingPongRotation()
    {
        LerpRotation();
        ChangeLerpDirection();
    }
    private void SetMinAndMaxRotation()
    {
        _minRotation = Quaternion.Euler(0, 0, -_angle);
        _maxRotation = Quaternion.Euler(0, 0, _angle);
    }
    private void ChangeLerpDirection()
    {
        if (_lerpValue >= 1f)
        {
            _direction = -1;
        }
        else if (_lerpValue <= 0)
        {
            _direction = 1;
        }
    }
    private void LerpRotation()
    {
        _lerpValue += _direction * _speed * Time.deltaTime;
        _enemyBody.rotation = Quaternion.Lerp(_minRotation, _maxRotation, _animationCurve.Evaluate(_lerpValue));
    }
}
