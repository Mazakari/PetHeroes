using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    public Rigidbody2D Rigidbody => _rb;
    private Transform _platform;

    [SerializeField] private  float _force = 10f;
    public float Force => _force;

    private bool _isActive = false;
    private Vector2 _startDirection = Vector2.zero;

    private IInputService _inputService;

    private void OnEnable()
    {
        _inputService = AllServices.Container.Single<IInputService>();
        InitRigidbodyAndStartDirection();

        _inputService.InputActions.Ball.LaunchBall.started += LaunchBall;
    }

    private void OnDisable()
    {
        _inputService.InputActions.Ball.LaunchBall.started -= LaunchBall;
    }

    void Update()
    {
        if (_isActive == true)
        {
            return;
        }

        FollowPlatformXPosition();
    }

    
    public void SetPlatformReference(Transform platform) =>
       _platform = platform;

    public void AddPlayerLaunchForce(Vector2 direction) =>
        _rb.AddForce(direction, ForceMode2D.Impulse);

    public void InitRigidbodyAndStartDirection()
    {
        _isActive = false;
        _rb.velocity = Vector2.zero;
        SetStartLaunchDirection();
        SetRigidbodyToKinematic();
    }

    private void SetStartLaunchDirection() =>
        _startDirection = new Vector2(0, 1 * _force);

    private void SetRigidbodyToKinematic()
    {
        _rb.isKinematic = true;
        _isActive = false;
    }

    private void FollowPlatformXPosition()
    {
        Vector2 playerPos = new Vector2(_platform.position.x, transform.position.y);
        transform.position = playerPos;
    }
    private void Launch()
    {
        SetRigidbodyToDynamic();
        AddPlayerLaunchForce(_startDirection);

        _isActive = true;
    }

    private void SetRigidbodyToDynamic() => 
        _rb.isKinematic = false;

    private void LaunchBall(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_isActive == false)
        {
            Launch();
        }
    }
}
