using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Transform _platform;

    [SerializeField] private  float Force = 800f;
    [SerializeField] private  float OffSetX = 100f;

    private bool _isActive = false;

    private void Awake() => 
        SetRigidbodyToKinematic();


    void Update()
    {
        if (_isActive == true)
        {
            return;
        }

        FollowPlatformXPosition();
        ReadPushInput();
    }
    public void SetPlatformReference(Transform platform) =>
       _platform = platform;

    private void SetRigidbodyToKinematic()
    {
        rb.isKinematic = true;
        _isActive = false;
    }

    private void FollowPlatformXPosition()
    {
        Vector2 playerPos = new Vector2(_platform.position.x, transform.position.y);
        transform.position = playerPos;
    }
    private void ReadPushInput()
    {
        if (Input.GetButtonDown(GlobalStringVars.PUSH))
        {
            SetRigidbodyToDynamic();
            AddPlayerLaunchForce();
            _isActive = true;
        }
    }

    private void SetRigidbodyToDynamic() => 
        rb.isKinematic = false;
    private void AddPlayerLaunchForce() => 
        rb.AddForce(new Vector2(OffSetX, Force));
}
