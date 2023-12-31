using UnityEngine;

public class PlatformClampMovement : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _platformCollider;
    private float _xPlatformBound;
    private float _platformXSize;

    private void OnEnable() =>
        GetHorizontalClampValues();

    void Update() => 
        ClampHorizontalMovement();
    private void GetHorizontalClampValues()
    {
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        _xPlatformBound = width / 2f;

        _platformXSize = _platformCollider.size.x;
        _xPlatformBound -= _platformXSize / 2f;
    }
    private void ClampHorizontalMovement() =>
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_xPlatformBound, _xPlatformBound), transform.position.y, transform.position.z);
}
