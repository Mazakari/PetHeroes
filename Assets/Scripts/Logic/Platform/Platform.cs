using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformInput _input;
    private float _xPlatformBound;
    private float _platformXSize;

    private void OnEnable() => 
        GetHorizontalClampValues();

    void Update()
    {
        //MovePlatform();
        ClampHorizontalMovement();
    }
    private void GetHorizontalClampValues()
    {
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        _xPlatformBound = width / 2f;

        _platformXSize = transform.localScale.x;
        _xPlatformBound -= _platformXSize / 2f;
    }

    //public void MovePlatform(Vector2 direction) =>
    //   transform.Translate(_speed * direction * Time.deltaTime * Vector3.right);

    private void ClampHorizontalMovement() => 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_xPlatformBound, _xPlatformBound), transform.position.y, transform.position.z);
}
